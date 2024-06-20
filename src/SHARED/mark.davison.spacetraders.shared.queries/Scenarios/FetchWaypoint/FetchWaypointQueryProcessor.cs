namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchWaypoint;

public sealed class FetchWaypointQueryProcessor(
    ISpacetradersDbContext dbContext,
    ISpaceTradersApiClient apiClient
) : IdentifiedQueryProcessor<FetchWaypointQueryRequest, FetchWaypointQueryResponse>(
    dbContext,
    apiClient)
{
    protected override async Task<FetchWaypointQueryResponse> ProcessAsyncIdentified(FetchWaypointQueryRequest request, ISpaceTradersApiClient apiClient, Guid userId, CancellationToken cancellationToken)
    {
        var apiResponse = await apiClient.GetWaypointAsync(
            request.SystemSymbol,
            request.WaypointSymbol,
            cancellationToken);

        return new FetchWaypointQueryResponse
        {
            Value = WaypointHelpers.ToWaypointResponse(apiResponse.Data)
        };
    }
}
