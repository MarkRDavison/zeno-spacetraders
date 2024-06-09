namespace mark.davison.spacetraders.shared.commands.Scenarios.OrbitShip;

public sealed class OrbitShipCommandProcessor : ICommandProcessor<OrbitShipCommandRequest, OrbitShipCommandResponse>
{
    private readonly ISpacetradersDbContext _dbContext;
    private readonly ISpaceTradersApiClient _apiClient;

    public OrbitShipCommandProcessor(
        ISpacetradersDbContext dbContext,
        ISpaceTradersApiClient apiClient)
    {
        _dbContext = dbContext;
        _apiClient = apiClient;
    }

    public async Task<OrbitShipCommandResponse> ProcessAsync(OrbitShipCommandRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        var account = await _dbContext.GetByIdAsync<Account>(request.AccountId, cancellationToken);

        if (account == null)
        {
            return ValidationMessages.CreateErrorResponse<OrbitShipCommandResponse>(
                ValidationMessages.INVALID_PROPERTY,
                nameof(OrbitShipCommandRequest.AccountId));
        }

        _apiClient.Token = account.Token;

        var apiResponse = await _apiClient.OrbitShipAsync(request.ShipSymbol, cancellationToken);

        return new OrbitShipCommandResponse
        {
            Value = ShipHelpers.ToShipNavDto(apiResponse.Data.Nav),
            ShipSymbol = request.ShipSymbol
        };
    }
}
