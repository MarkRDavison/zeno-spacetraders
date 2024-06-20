namespace mark.davison.spacetraders.shared.models.dtos.Queries.FetchWaypoint;

[GetRequest(Path = "fetch-waypoint-query")]
public sealed class FetchWaypointQueryRequest : IdentifiedQueryRequest<FetchWaypointQueryRequest, FetchWaypointQueryResponse>
{
    public string SystemSymbol { get; set; } = string.Empty;
    public string WaypointSymbol { get; set; } = string.Empty;
}
