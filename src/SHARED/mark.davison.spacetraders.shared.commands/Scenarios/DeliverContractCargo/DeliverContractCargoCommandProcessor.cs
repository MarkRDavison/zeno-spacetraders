namespace mark.davison.spacetraders.shared.commands.Scenarios.DeliverContractCargo;

public sealed class DeliverContractCargoCommandProcessor(
    ISpacetradersDbContext dbContext,
    ISpaceTradersApiClient apiClient
) : IdentifiedCommandProcessor<DeliverContractCargoCommandRequest, DeliverContractCargoCommandResponse>(
    dbContext,
    apiClient)
{
    protected override async Task<DeliverContractCargoCommandResponse> ProcessAsyncIdentified(DeliverContractCargoCommandRequest request, ISpaceTradersApiClient apiClient, Guid userId, CancellationToken cancellationToken)
    {
        var apiResponse = await apiClient.DeliverContractAsync(
            new()
            {
                ShipSymbol = request.ShipSymbol,
                TradeSymbol = request.TradeSymbol,
                Units = request.Units
            },
            request.ContractId,
            cancellationToken);

        return new DeliverContractCargoCommandResponse
        {
            Value = new MultiResponse
            {
                AgentDto = null,
                ContractDto = ContractHelpers.ToContractDto(apiResponse.Data.Contract),
                ShipResponse = new ShipResponse
                {
                    Ship = null,
                    Cooldown = null,
                    Fuel = null,
                    ShipNav = null,
                    ShipNavRoute = null,
                    ShipCargo = ShipHelpers.ToShipCargoDto(request.ShipSymbol, apiResponse.Data.Cargo)
                }
            }
        };
    }
}
