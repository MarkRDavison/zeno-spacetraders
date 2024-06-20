namespace mark.davison.spacetraders.shared.commands.Scenarios.RefuelShip;

public sealed class RefuelShipCommandProcessor(
    ISpacetradersDbContext dbContext,
    ISpaceTradersApiClient apiClient
) : IdentifiedCommandProcessor<RefuelShipCommandRequest, RefuelShipCommandResponse>(
    dbContext,
    apiClient)
{
    protected override async Task<RefuelShipCommandResponse> ProcessAsyncIdentified(RefuelShipCommandRequest request, ISpaceTradersApiClient apiClient, Guid userId, CancellationToken cancellationToken)
    {
        var apiResponse = await apiClient.RefuelShipAsync(
            new()
            {
                FromCargo = request.FromCargo,
                Units = request.Units
            },
            request.ShipSymbol,
            cancellationToken);

        return new RefuelShipCommandResponse
        {
            Value = new MultiResponse
            {
                AgentDto = AgentHelpers.ToAgentDto(apiResponse.Data.Agent),
                ShipResponse = new ShipResponse
                {
                    Fuel = ShipHelpers.ToFuelDto(request.ShipSymbol, apiResponse.Data.Fuel),
                    Cooldown = null,
                    Ship = null,
                    ShipCargo = null, // TODO: Cargo if fueled from cargo may have changed
                    ShipNav = null,
                    ShipNavRoute = null
                }
            }
        };
    }
}
