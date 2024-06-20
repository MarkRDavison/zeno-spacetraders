namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchShip;

public sealed class FetchShipQueryProcessor(
    ISpacetradersDbContext dbContext,
    ISpaceTradersApiClient apiClient
) : IdentifiedQueryProcessor<FetchShipQueryRequest, FetchShipQueryResponse>(
    dbContext,
    apiClient)
{
    protected override async Task<FetchShipQueryResponse> ProcessAsyncIdentified(FetchShipQueryRequest request, ISpaceTradersApiClient apiClient, Guid userId, CancellationToken cancellationToken)
    {
        var apiResponse = await apiClient.GetMyShipAsync(request.ShipSymbol, cancellationToken);

        return new FetchShipQueryResponse
        {
            Value = ShipHelpers.ToShipResponse(apiResponse.Data)
        };
    }
}
