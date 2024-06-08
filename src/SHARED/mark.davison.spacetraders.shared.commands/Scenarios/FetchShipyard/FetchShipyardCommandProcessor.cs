namespace mark.davison.spacetraders.shared.commands.Scenarios.FetchShipyard;

public sealed class FetchShipyardCommandProcessor : ICommandProcessor<FetchShipyardCommandRequest, FetchShipyardCommandResponse>
{
    private readonly ISpacetradersDbContext _dbContext;
    private readonly ISpaceTradersApiClient _apiClient;

    public FetchShipyardCommandProcessor(
        ISpacetradersDbContext dbContext,
        ISpaceTradersApiClient apiClient)
    {
        _dbContext = dbContext;
        _apiClient = apiClient;
    }

    public async Task<FetchShipyardCommandResponse> ProcessAsync(FetchShipyardCommandRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        var account = await _dbContext.GetByIdAsync<Account>(request.AccountId, cancellationToken);

        if (account == null)
        {
            return ValidationMessages.CreateErrorResponse<FetchShipyardCommandResponse>(
                ValidationMessages.INVALID_PROPERTY,
                nameof(FetchShipyardCommandRequest.AccountId));
        }

        _apiClient.Token = account.Token;

        var apiResponse = await _apiClient.GetShipyardAsync(request.SystemSymbol, request.WaypointSymbol, cancellationToken);

        return new FetchShipyardCommandResponse
        {
            Value = FromShipyard(apiResponse.Data)
        };
    }

    private ShipyardDto FromShipyard(Shipyard shipyard)
    {
        return new ShipyardDto
        {
            Symbol = shipyard.Symbol,
            ModificationsFee = shipyard.ModificationsFee,
            ShipTypes = shipyard.ShipTypes == null ? [] : [.. shipyard.ShipTypes.Select(_ => _.Type.ToString())],
            ShipyardShips = shipyard.Ships == null ? [] : [.. shipyard.Ships.Select(FromShipyardShip)],
            ShipyardTransactions = shipyard.Transactions == null ? [] : [.. shipyard.Transactions.Select(FromShipyardTransaction)]
        };
    }

    private ShipyardShipDto FromShipyardShip(ShipyardShip shipyardShip)
    {
        return new ShipyardShipDto
        {
            Type = shipyardShip.Type.ToString(),
            Name = shipyardShip.Name,
            Description = shipyardShip.Description,
            PurchasePrice = shipyardShip.PurchasePrice,
            Supply = shipyardShip.Supply.ToString()
        };
    }
    private ShipyardTransactionDto FromShipyardTransaction(ShipyardTransaction shipyardTransaction)
    {
        return new ShipyardTransactionDto
        {
            Type = shipyardTransaction.ShipType.ToString(),
            AgentSymbol = shipyardTransaction.AgentSymbol,
            Price = shipyardTransaction.Price
        };
    }
}
