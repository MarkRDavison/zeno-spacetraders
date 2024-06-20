namespace mark.davison.spacetraders.shared.commands.Scenarios.ModifyOrbit;

public sealed class ModifyOrbitCommandProcessor(
    ISpacetradersDbContext dbContext,
    ISpaceTradersApiClient apiClient
) : IdentifiedCommandProcessor<ModifyOrbitCommandRequest, ModifyOrbitCommandResponse>(
    dbContext,
    apiClient)
{
    protected override async Task<ModifyOrbitCommandResponse> ProcessAsyncIdentified(ModifyOrbitCommandRequest request, ISpaceTradersApiClient apiClient, Guid userId, CancellationToken cancellationToken)
    {
        if (request.Dock)
        {
            var apiResponse = await apiClient.DockShipAsync(request.ShipSymbol, cancellationToken);

            return new ModifyOrbitCommandResponse
            {
                Value = new ShipResponse
                {
                    Ship = null,
                    ShipCargo = null,
                    ShipNav = ShipHelpers.ToShipNavDto(request.ShipSymbol, apiResponse.Data.Nav),
                    ShipNavRoute = ShipHelpers.ToShipNavRouteDto(request.ShipSymbol, apiResponse.Data.Nav.Route),
                    Cooldown = null,
                    Fuel = null
                }
            };
        }
        else
        {
            var apiResponse = await apiClient.OrbitShipAsync(request.ShipSymbol, cancellationToken);

            return new ModifyOrbitCommandResponse
            {
                Value = new ShipResponse
                {
                    Ship = null,
                    ShipCargo = null,
                    ShipNav = ShipHelpers.ToShipNavDto(request.ShipSymbol, apiResponse.Data.Nav),
                    ShipNavRoute = ShipHelpers.ToShipNavRouteDto(request.ShipSymbol, apiResponse.Data.Nav.Route),
                    Cooldown = null,
                    Fuel = null
                }
            };
        }
    }
}
