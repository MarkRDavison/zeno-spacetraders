namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchShips;

public sealed class FetchShipsQueryProcessor(
    ISpacetradersDbContext dbContext,
    ISpaceTradersApiClient apiClient
) : IdentifiedQueryProcessor<FetchShipsQueryRequest, FetchShipsQueryResponse>(
    dbContext,
    apiClient)
{
    protected override async Task<FetchShipsQueryResponse> ProcessAsyncIdentified(FetchShipsQueryRequest request, ISpaceTradersApiClient apiClient, Guid userId, CancellationToken cancellationToken)
    {
        var apiResponse = await apiClient.GetMyShipsAsync(request.Page, request.Limit, cancellationToken);

        return new FetchShipsQueryResponse
        {
            Value = [.. apiResponse.Data.Select(ShipHelpers.ToShipResponse)]
        };
    }
}
