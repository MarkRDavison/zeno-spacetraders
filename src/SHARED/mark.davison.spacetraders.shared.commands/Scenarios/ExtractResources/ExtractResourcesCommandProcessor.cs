namespace mark.davison.spacetraders.shared.commands.Scenarios.ExtractResources;

public sealed class ExtractResourcesCommandProcessor(
    ISpacetradersDbContext dbContext,
    ISpaceTradersApiClient apiClient
) : IdentifiedCommandProcessor<ExtractResourcesCommandRequest, ExtractResourcesCommandResponse>(
    dbContext,
    apiClient)
{
    protected override async Task<ExtractResourcesCommandResponse> ProcessAsyncIdentified(ExtractResourcesCommandRequest request, ISpaceTradersApiClient apiClient, Guid userId, CancellationToken cancellationToken)
    {
        var apiResponse = await apiClient.ExtractResourcesAsync(new(), request.ShipSymbol, cancellationToken);

        return new ExtractResourcesCommandResponse
        {
            Value = new ShipResponse
            {
                Cooldown = ShipHelpers.ToCooldownDto(request.ShipSymbol, apiResponse.Data.Cooldown),
                ShipCargo = ShipHelpers.ToShipCargoDto(request.ShipSymbol, apiResponse.Data.Cargo),
                Ship = null,
                ShipNav = null,
                ShipNavRoute = null,
                Fuel = null
            }
        };
    }
}
