namespace mark.davison.spacetraders.shared.commands.Scenarios.ModifyShipState;

public sealed class ModifyShipStateCommandProcessor(
    ISpacetradersDbContext dbContext,
    ISpacetradersApiClient apiClient
) : IdentifiedCommandProcessor<ModifyShipStateCommandRequest, ModifyShipStateCommandResponse>(
    dbContext,
    apiClient)
{
    protected override async Task<ModifyShipStateCommandResponse> ProcessAsyncIdentified(ModifyShipStateCommandRequest request, ISpacetradersApiClient apiClient, Guid userId, CancellationToken cancellationToken)
    {
        if (request.SetDocked && request.SetOrbiting)
        {
            return ValidationMessages.CreateErrorResponse<ModifyShipStateCommandResponse>(
                ValidationMessages.BAD_REQUEST);
        }

        if (request.SetDocked)
        {
            var response = await apiClient.DockShipAsync(request.ShipSymbol, cancellationToken);

            return new ModifyShipStateCommandResponse
            {
                Value = new ShipResponse
                {
                    ShipNav = response.Data.Nav.Adapt(request.ShipSymbol)
                }
            };
        }

        if (request.SetOrbiting)
        {
            var response = await apiClient.OrbitShipAsync(request.ShipSymbol, cancellationToken);

            return new ModifyShipStateCommandResponse
            {
                Value = new ShipResponse
                {
                    ShipNav = response.Data.Nav.Adapt(request.ShipSymbol)
                }
            };
        }

        return ValidationMessages.CreateErrorResponse<ModifyShipStateCommandResponse>(
            ValidationMessages.BAD_REQUEST);
    }
}
