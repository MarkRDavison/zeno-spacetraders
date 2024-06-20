namespace mark.davison.spacetraders.shared.commands.Scenarios.NavigateShip;

public sealed class NavigateShipCommandProcessor(
    ISpacetradersDbContext dbContext,
    ISpaceTradersApiClient apiClient
) : IdentifiedCommandProcessor<NavigateShipCommandRequest, NavigateShipCommandResponse>(
    dbContext,
    apiClient)
{
    protected override async Task<NavigateShipCommandResponse> ProcessAsyncIdentified(NavigateShipCommandRequest request, ISpaceTradersApiClient apiClient, Guid userId, CancellationToken cancellationToken)
    {
        var apiResponse = await apiClient.NavigateShipAsync(
            new()
            {
                WaypointSymbol = request.DestinationWaypoint
            },
            request.ShipSymbol,
            cancellationToken);

        return new NavigateShipCommandResponse
        {
            Value = ShipHelpers.ToShipResponse(new Ship
            {
                Symbol = request.ShipSymbol,
                Nav = apiResponse.Data.Nav,
                Fuel = apiResponse.Data.Fuel
            })
        };
    }
}
