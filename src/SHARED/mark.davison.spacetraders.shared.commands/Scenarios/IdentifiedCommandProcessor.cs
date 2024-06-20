using Response = mark.davison.common.CQRS.Response;

namespace mark.davison.spacetraders.shared.commands.Scenarios;

public abstract class IdentifiedCommandProcessor<TRequest, TResponse> : ICommandProcessor<TRequest, TResponse>
    where TRequest : IdentifiedCommandRequest<TRequest, TResponse>
    where TResponse : Response, new()
{
    private readonly ISpacetradersDbContext _dbContext;
    private readonly ISpaceTradersApiClient _apiClient;

    public IdentifiedCommandProcessor(
        ISpacetradersDbContext dbContext,
        ISpaceTradersApiClient apiClient)
    {
        _dbContext = dbContext;
        _apiClient = apiClient;
    }

    public async Task<TResponse> ProcessAsync(TRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        var account = await _dbContext
            .Set<Account>()
            .Where(_ => _.Identifier == request.Identifier)
            .FirstOrDefaultAsync(cancellationToken);

        if (account == null)
        {
            return ValidationMessages.CreateErrorResponse<TResponse>(
                ValidationMessages.INVALID_PROPERTY,
                nameof(Account.Identifier));
        }

        _apiClient.Token = account.Token;

        try
        {
            return await ProcessAsyncIdentified(request, _apiClient, currentUserContext.CurrentUser.Id, cancellationToken);
        }
        catch (ApiException apiException)
        {
            // TODO: Deserialize apiException.Response 
            throw;
        }
    }

    protected abstract Task<TResponse> ProcessAsyncIdentified(TRequest request, ISpaceTradersApiClient apiClient, Guid userId, CancellationToken cancellationToken);
}
