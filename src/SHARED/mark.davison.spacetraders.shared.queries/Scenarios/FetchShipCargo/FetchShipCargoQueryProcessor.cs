namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchShipCargo;

public sealed class FetchShipCargoQueryProcessor : IQueryProcessor<FetchShipCargoQueryRequest, FetchShipCargoQueryResponse>
{
    private readonly ISpacetradersDbContext _dbContext;
    private readonly ISpaceTradersApiClient _apiClient;

    public FetchShipCargoQueryProcessor(
        ISpacetradersDbContext dbContext,
        ISpaceTradersApiClient apiClient)
    {
        _dbContext = dbContext;
        _apiClient = apiClient;
    }

    public async Task<FetchShipCargoQueryResponse> ProcessAsync(FetchShipCargoQueryRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        var account = await _dbContext.GetByIdAsync<Account>(request.AccountId, cancellationToken);

        if (account == null)
        {
            return ValidationMessages.CreateErrorResponse<FetchShipCargoQueryResponse>(
                ValidationMessages.INVALID_PROPERTY,
                nameof(FetchShipCargoQueryRequest.AccountId));
        }

        _apiClient.Token = account.Token;

        var apiResponse = await _apiClient.GetMyShipCargoAsync(request.ShipSymbol, cancellationToken);

        var cargo = ShipHelpers.ToShipCargoDto(apiResponse.Data);
        cargo.ShipSymbol = request.ShipSymbol;
        return new FetchShipCargoQueryResponse
        {
            Value = cargo
        };
    }
}
