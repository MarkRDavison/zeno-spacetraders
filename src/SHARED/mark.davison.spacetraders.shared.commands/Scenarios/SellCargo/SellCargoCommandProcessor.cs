namespace mark.davison.spacetraders.shared.commands.Scenarios.SellCargo;

public sealed class SellCargoCommandProcessor(
    ISpacetradersDbContext dbContext,
    ISpaceTradersApiClient apiClient
) : IdentifiedCommandProcessor<SellCargoCommandRequest, SellCargoCommandResponse>(
    dbContext,
    apiClient)
{
    protected override async Task<SellCargoCommandResponse> ProcessAsyncIdentified(SellCargoCommandRequest request, ISpaceTradersApiClient apiClient, Guid userId, CancellationToken cancellationToken)
    {
        var apiResponse = await apiClient.SellCargoAsync(
            new SellCargoRequest
            {
                Symbol = Enum.Parse<TradeSymbol>(request.TradeSymbol, true),
                Units = request.Units
            },
            request.ShipSymbol,
            cancellationToken);

        // TODO: Cannot sell many items in each place

        return new SellCargoCommandResponse
        {
            Value = new MultiResponse
            {
                AgentDto = AgentHelpers.ToAgentDto(apiResponse.Data.Agent),
                ShipResponse = ShipHelpers.ToShipResponse(new Ship
                {
                    Symbol = request.ShipSymbol,
                    Cargo = apiResponse.Data.Cargo
                })
            }
        };
    }
}
