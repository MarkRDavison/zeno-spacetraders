namespace mark.davison.spacetraders.shared.models.dtos.Queries.FetchWaypoints;

[GetRequest(Path = "fetch-waypoints-query")]
public sealed class FetchWaypointsQueryRequest : PaginatedIdentifiedQueryRequest<FetchWaypointsQueryRequest, FetchWaypointsQueryResponse>
{
    public string SystemSymbol { get; set; } = string.Empty;
    public string? WaypointType { get; set; }
    public string? WaypointTrait { get; set; }
}
