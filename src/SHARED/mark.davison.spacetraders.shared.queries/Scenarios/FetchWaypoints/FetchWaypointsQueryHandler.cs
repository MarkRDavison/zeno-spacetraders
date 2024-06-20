namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchWaypoints;

public sealed class FetchWaypointsQueryHandler(
    IQueryProcessor<FetchWaypointsQueryRequest, FetchWaypointsQueryResponse> processor
) : ValidateAndProcessQueryHandler<FetchWaypointsQueryRequest, FetchWaypointsQueryResponse>(
    processor)
{
}
