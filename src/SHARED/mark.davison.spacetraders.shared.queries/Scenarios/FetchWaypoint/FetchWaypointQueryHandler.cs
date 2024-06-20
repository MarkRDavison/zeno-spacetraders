namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchWaypoint;

public sealed class FetchWaypointQueryHandler(
    IQueryProcessor<FetchWaypointQueryRequest, FetchWaypointQueryResponse> processor
) : ValidateAndProcessQueryHandler<FetchWaypointQueryRequest, FetchWaypointQueryResponse>(
    processor)
{
}
