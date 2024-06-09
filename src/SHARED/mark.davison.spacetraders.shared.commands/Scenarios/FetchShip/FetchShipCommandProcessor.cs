namespace mark.davison.spacetraders.shared.commands.Scenarios.FetchShip;

public sealed class FetchShipCommandProcessor : ICommandProcessor<FetchShipCommandRequest, FetchShipCommandResponse>
{
    private readonly ISpacetradersDbContext _dbContext;
    private readonly ISpaceTradersApiClient _apiClient;

    public FetchShipCommandProcessor(
        ISpacetradersDbContext dbContext,
        ISpaceTradersApiClient apiClient)
    {
        _dbContext = dbContext;
        _apiClient = apiClient;
    }

    public async Task<FetchShipCommandResponse> ProcessAsync(FetchShipCommandRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        var account = await _dbContext.GetByIdAsync<Account>(request.AccountId, cancellationToken);

        if (account == null)
        {
            return ValidationMessages.CreateErrorResponse<FetchShipCommandResponse>(
                ValidationMessages.INVALID_PROPERTY,
                nameof(FetchShipCommandRequest.AccountId));
        }

        _apiClient.Token = account.Token;

        var apiResponse = await _apiClient.GetMyShipAsync(request.ShipSymbol, cancellationToken);

        return new FetchShipCommandResponse
        {
            Value = ShipHelpers.ToShipDto(apiResponse.Data)
        };
    }
}
