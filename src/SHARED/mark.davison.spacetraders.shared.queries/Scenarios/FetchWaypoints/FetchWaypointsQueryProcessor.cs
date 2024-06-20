namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchWaypoints;

public sealed class FetchWaypointsQueryProcessor(
    ISpacetradersDbContext dbContext,
    ISpaceTradersApiClient apiClient
) : IdentifiedQueryProcessor<FetchWaypointsQueryRequest, FetchWaypointsQueryResponse>(
    dbContext,
    apiClient)
{
    protected override async Task<FetchWaypointsQueryResponse> ProcessAsyncIdentified(FetchWaypointsQueryRequest request, ISpaceTradersApiClient apiClient, Guid userId, CancellationToken cancellationToken)
    {
        WaypointType? type = null;
        if (Enum.TryParse<WaypointType>(request.WaypointType, true, out var validType))
        {
            type = validType;
        }

        WaypointTraitSymbol? trait = null;
        if (Enum.TryParse<WaypointTraitSymbol>(request.WaypointTrait, true, out var validTrait))
        {
            trait = validTrait;
        }

        var apiResponse = await apiClient.GetSystemWaypointsAsync(
            request.Page,
            request.Limit,
            type,
            trait,
            request.SystemSymbol,
            cancellationToken);

        return new FetchWaypointsQueryResponse
        {
            Value = [.. apiResponse.Data.Select(WaypointHelpers.ToWaypointResponse)]
        };
    }
}
