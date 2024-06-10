
namespace mark.davison.spacetraders.shared.commands.Scenarios.NavigateShip;

public sealed class NavigateShipCommandProcessor : ICommandProcessor<NavigateShipCommandRequest, NavigateShipCommandResponse>
{
    private readonly ISpacetradersDbContext _dbContext;
    private readonly ISpaceTradersApiClient _apiClient;

    public NavigateShipCommandProcessor(
        ISpacetradersDbContext dbContext,
        ISpaceTradersApiClient apiClient)
    {
        _dbContext = dbContext;
        _apiClient = apiClient;
    }

    public async Task<NavigateShipCommandResponse> ProcessAsync(NavigateShipCommandRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        var account = await _dbContext.GetByIdAsync<Account>(request.AccountId, cancellationToken);

        if (account == null)
        {
            return ValidationMessages.CreateErrorResponse<NavigateShipCommandResponse>(
                ValidationMessages.INVALID_PROPERTY,
                nameof(NavigateShipCommandRequest.AccountId));
        }

        _apiClient.Token = account.Token;

        var apiResponse = await _apiClient.NavigateShipAsync(
            new()
            {
                WaypointSymbol = request.DestinationWaypoint
            },
            request.ShipSymbol,
            cancellationToken);

        return new NavigateShipCommandResponse
        {
            Value = (ShipHelpers.ToShipNavDto(apiResponse.Data.Nav), ShipHelpers.ToShipFuelDto(apiResponse.Data.Fuel))
        };
    }
}
