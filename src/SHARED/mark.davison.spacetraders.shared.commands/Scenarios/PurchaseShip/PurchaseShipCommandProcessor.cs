namespace mark.davison.spacetraders.shared.commands.Scenarios.PurchaseShip;

public sealed class PurchaseShipCommandProcessor(
    ISpacetradersDbContext dbContext,
    ISpaceTradersApiClient apiClient
) : IdentifiedCommandProcessor<PurchaseShipCommandRequest, PurchaseShipCommandResponse>(
    dbContext,
    apiClient)
{
    protected override async Task<PurchaseShipCommandResponse> ProcessAsyncIdentified(PurchaseShipCommandRequest request, ISpaceTradersApiClient apiClient, Guid userId, CancellationToken cancellationToken)
    {
        var apiResponse = await apiClient.PurchaseShipAsync(new()
        {
            WaypointSymbol = request.WaypointSymbol,
            ShipType = Enum.Parse<ShipType>(request.ShipType, true)
        }, cancellationToken);

        return new PurchaseShipCommandResponse
        {
            Value = new()
            {
                AgentDto = AgentHelpers.ToAgentDto(apiResponse.Data.Agent),
                ShipResponse = ShipHelpers.ToShipResponse(apiResponse.Data.Ship)
            }
        };
    }
}
