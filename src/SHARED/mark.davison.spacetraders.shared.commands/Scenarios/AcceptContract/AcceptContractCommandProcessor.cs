namespace mark.davison.spacetraders.shared.commands.Scenarios.AcceptContract;

public sealed class AcceptContractCommandProcessor : ICommandProcessor<AcceptContractCommandRequest, AcceptContractCommandResponse>
{
    private readonly ISpacetradersDbContext _dbContext;
    private readonly ISpaceTradersApiClient _apiClient;

    public AcceptContractCommandProcessor(
        ISpacetradersDbContext dbContext,
        ISpaceTradersApiClient apiClient)
    {
        _dbContext = dbContext;
        _apiClient = apiClient;
    }

    public async Task<AcceptContractCommandResponse> ProcessAsync(AcceptContractCommandRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        var account = await _dbContext.GetByIdAsync<Account>(request.AccountId, cancellationToken);

        if (account == null)
        {
            return ValidationMessages.CreateErrorResponse<AcceptContractCommandResponse>(
                ValidationMessages.INVALID_PROPERTY,
                nameof(AcceptContractCommandRequest.AccountId));
        }

        _apiClient.Token = account.Token;

        var apiResponse = await _apiClient.AcceptContractAsync(request.ContractId, cancellationToken);

        return new AcceptContractCommandResponse
        {
            Value = ContractHelpers.ToContractDto(apiResponse.Data.Contract),
            Credits = apiResponse.Data.Agent.Credits
        };
    }
}
