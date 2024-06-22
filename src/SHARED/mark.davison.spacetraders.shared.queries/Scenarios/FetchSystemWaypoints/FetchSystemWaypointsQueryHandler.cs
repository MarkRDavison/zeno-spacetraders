namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchSystemWaypoints;

public sealed class FetchSystemWaypointsQueryHandler(
    IQueryProcessor<FetchSystemWaypointsQueryRequest, FetchSystemWaypointsQueryResponse> processor
) : ValidateAndProcessQueryHandler<FetchSystemWaypointsQueryRequest, FetchSystemWaypointsQueryResponse>(
    processor)
{
}
