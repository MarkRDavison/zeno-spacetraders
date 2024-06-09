namespace mark.davison.spacetraders.shared.commands.Scenarios.PurchaseShip;

public sealed class PurchaseShipCommandProcessor : ICommandProcessor<PurchaseShipCommandRequest, PurchaseShipCommandResponse>
{
    private readonly ISpacetradersDbContext _dbContext;
    private readonly ISpaceTradersApiClient _apiClient;

    public PurchaseShipCommandProcessor(
        ISpacetradersDbContext dbContext,
        ISpaceTradersApiClient apiClient)
    {
        _dbContext = dbContext;
        _apiClient = apiClient;
    }

    public async Task<PurchaseShipCommandResponse> ProcessAsync(PurchaseShipCommandRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        var account = await _dbContext.GetByIdAsync<Account>(request.AccountId, cancellationToken);

        if (account == null)
        {
            return ValidationMessages.CreateErrorResponse<PurchaseShipCommandResponse>(
                ValidationMessages.INVALID_PROPERTY,
                nameof(PurchaseShipCommandRequest.AccountId));
        }

        var shipType = Enum.Parse<ShipType>(request.ShipType);

        _apiClient.Token = account.Token;

        // TODO: Validation to make sure you can afford it?
        var apiResponse = await _apiClient.PurchaseShipAsync(new Body4
        {
            ShipType = shipType,
            WaypointSymbol = request.WaypointSymbol
        }, cancellationToken);

        return new PurchaseShipCommandResponse
        {
            Value = ShipHelpers.ToShipDto(apiResponse.Data.Ship),
            Credits = apiResponse.Data.Agent.Credits
        };
    }
}
