namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchShips;

public sealed class FetchShipsQueryProcessor(
    ISpacetradersDbContext dbContext,
    ISpacetradersApiClient apiClient
) : IdentifiedQueryProcessor<FetchShipsQueryRequest, FetchShipsQueryResponse>(
    dbContext,
    apiClient)
{
    protected override async Task<FetchShipsQueryResponse> ProcessAsyncIdentified(FetchShipsQueryRequest request, ISpacetradersApiClient apiClient, Guid userId, CancellationToken cancellationToken)
    {
        // TODO: Handle request.All
        var response = await apiClient.GetMyShipsAsync(
            request.Page,
            request.Limit,
            cancellationToken);

        return new FetchShipsQueryResponse
        {
            Value = [.. response.Data.Select(ShipAdapters.AdaptResponse)]
        };
    }
}
