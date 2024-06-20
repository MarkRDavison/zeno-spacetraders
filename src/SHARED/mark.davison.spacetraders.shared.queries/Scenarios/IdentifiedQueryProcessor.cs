using Response = mark.davison.common.CQRS.Response;

namespace mark.davison.spacetraders.shared.queries.Scenarios;

public abstract class IdentifiedQueryProcessor<TRequest, TResponse> : IQueryProcessor<TRequest, TResponse>
    where TRequest : IdentifiedQueryRequest<TRequest, TResponse>
    where TResponse : Response, new()
{
    private readonly ISpacetradersDbContext _dbContext;
    private readonly ISpaceTradersApiClient _apiClient;

    public IdentifiedQueryProcessor(
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

        return await ProcessAsyncIdentified(request, _apiClient, currentUserContext.CurrentUser.Id, cancellationToken);
    }

    protected abstract Task<TResponse> ProcessAsyncIdentified(TRequest request, ISpaceTradersApiClient apiClient, Guid userId, CancellationToken cancellationToken);
}
