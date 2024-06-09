namespace mark.davison.spacetraders.shared.commands.Scenarios.DockShip;

public sealed class DockShipCommandProcessor : ICommandProcessor<DockShipCommandRequest, DockShipCommandResponse>
{
    private readonly ISpacetradersDbContext _dbContext;
    private readonly ISpaceTradersApiClient _apiClient;

    public DockShipCommandProcessor(
        ISpacetradersDbContext dbContext,
        ISpaceTradersApiClient apiClient)
    {
        _dbContext = dbContext;
        _apiClient = apiClient;
    }

    public async Task<DockShipCommandResponse> ProcessAsync(DockShipCommandRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        var account = await _dbContext.GetByIdAsync<Account>(request.AccountId, cancellationToken);

        if (account == null)
        {
            return ValidationMessages.CreateErrorResponse<DockShipCommandResponse>(
                ValidationMessages.INVALID_PROPERTY,
                nameof(DockShipCommandRequest.AccountId));
        }

        _apiClient.Token = account.Token;

        var apiResponse = await _apiClient.DockShipAsync(request.ShipSymbol, cancellationToken);

        return new DockShipCommandResponse
        {
            Value = ShipHelpers.ToShipNavDto(apiResponse.Data.Nav),
            ShipSymbol = request.ShipSymbol
        };
    }
}
