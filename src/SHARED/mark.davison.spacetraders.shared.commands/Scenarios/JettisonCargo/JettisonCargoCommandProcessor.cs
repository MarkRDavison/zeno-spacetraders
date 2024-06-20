namespace mark.davison.spacetraders.shared.commands.Scenarios.JettisonCargo;

public sealed class JettisonCargoCommandProcessor(
    ISpacetradersDbContext dbContext,
    ISpaceTradersApiClient apiClient
) : IdentifiedCommandProcessor<JettisonCargoCommandRequest, JettisonCargoCommandResponse>(
    dbContext,
    apiClient)
{
    protected override async Task<JettisonCargoCommandResponse> ProcessAsyncIdentified(JettisonCargoCommandRequest request, ISpaceTradersApiClient apiClient, Guid userId, CancellationToken cancellationToken)
    {
        var apiResponse = await apiClient.JettisonAsync(
            new()
            {
                Symbol = Enum.Parse<TradeSymbol>(request.TradeSymbol, true),
                Units = request.Units
            },
            request.ShipSymbol,
            cancellationToken);

        return new JettisonCargoCommandResponse
        {
            Value = new()
            {
                Cooldown = null,
                Ship = null,
                ShipNav = null,
                ShipNavRoute = null,
                ShipCargo = ShipHelpers.ToShipCargoDto(request.ShipSymbol, apiResponse.Data.Cargo),
                Fuel = null
            }
        };
    }
}
