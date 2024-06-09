namespace mark.davison.spacetraders.shared.commands.Scenarios.PurchaseShip;

public sealed class PurchaseShipCommandValidator : ICommandValidator<PurchaseShipCommandRequest, PurchaseShipCommandResponse>
{
    private readonly ISpacetradersDbContext _dbContext;
    private readonly ISpaceTradersApiClient _apiClient;

    public PurchaseShipCommandValidator(
        ISpacetradersDbContext dbContext,
        ISpaceTradersApiClient apiClient)
    {
        _dbContext = dbContext;
        _apiClient = apiClient;
    }

    public async Task<PurchaseShipCommandResponse> ValidateAsync(PurchaseShipCommandRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        var account = await _dbContext.GetByIdAsync<Account>(request.AccountId, cancellationToken);

        if (account == null)
        {
            return ValidationMessages.CreateErrorResponse<PurchaseShipCommandResponse>(
                ValidationMessages.INVALID_PROPERTY,
                nameof(PurchaseShipCommandRequest.AccountId));
        }

        if (!Enum.TryParse<ShipType>(request.ShipType, out var shipType))
        {
            return ValidationMessages.CreateErrorResponse<PurchaseShipCommandResponse>(
                ValidationMessages.INVALID_PROPERTY,
                nameof(PurchaseShipCommandRequest.ShipType));
        }

        _apiClient.Token = account.Token;

        var agent = await _apiClient.GetMyAgentAsync(cancellationToken);

        await Task.Delay(TimeSpan.FromMilliseconds(500), cancellationToken);

        var shipyard = await _apiClient.GetShipyardAsync(
            WaypointsHelpers.GetSystemFromWaypoint(request.WaypointSymbol),
            request.WaypointSymbol,
            cancellationToken);

        var shipyardShip = shipyard.Data.Ships.FirstOrDefault(_ => _.Type == shipType);

        if (shipyardShip == null)
        {
            return ValidationMessages.CreateErrorResponse<PurchaseShipCommandResponse>(
                ValidationMessages.INVALID_PROPERTY,
                nameof(PurchaseShipCommandRequest.ShipType));
        }

        if (shipyardShip.PurchasePrice > agent.Data.Credits)
        {
            return ValidationMessages.CreateErrorResponse<PurchaseShipCommandResponse>(
                ValidationMessages.INSUFFICIENT_CREDITS);
        }

        await Task.Delay(TimeSpan.FromMilliseconds(500), cancellationToken);

        return new();
    }
}
