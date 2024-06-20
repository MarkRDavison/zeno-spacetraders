namespace mark.davison.spacetraders.shared.commands.Scenarios.FetchShipyard;

public sealed class FetchShipyardCommandProcessor : ICommandProcessor<FetchShipyardCommandRequest, FetchShipyardCommandResponse>
{
    private readonly ISpacetradersDbContext _dbContext;
    private readonly ISpaceTradersApiClient _apiClient;

    public FetchShipyardCommandProcessor(
        ISpacetradersDbContext dbContext,
        ISpaceTradersApiClient apiClient)
    {
        _dbContext = dbContext;
        _apiClient = apiClient;
    }

    public async Task<FetchShipyardCommandResponse> ProcessAsync(FetchShipyardCommandRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        var account = await _dbContext.GetByIdAsync<Account>(request.AccountId, cancellationToken);

        if (account == null)
        {
            return ValidationMessages.CreateErrorResponse<FetchShipyardCommandResponse>(
                ValidationMessages.INVALID_PROPERTY,
                nameof(FetchShipyardCommandRequest.AccountId));
        }

        _apiClient.Token = account.Token;

        var apiResponse = await _apiClient.GetShipyardAsync(request.SystemSymbol, request.WaypointSymbol, cancellationToken);

        return new FetchShipyardCommandResponse
        {
            Value = WaypointHelpers.FromShipyard(apiResponse.Data)
        };
    }
}
