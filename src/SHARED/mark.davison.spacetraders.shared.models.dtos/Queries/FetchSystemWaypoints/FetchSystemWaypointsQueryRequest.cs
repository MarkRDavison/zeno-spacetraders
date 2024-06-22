namespace mark.davison.spacetraders.shared.models.dtos.Queries.FetchSystemWaypoints;

[GetRequest(Path = "fetch-system-waypoints-query")]
public sealed class FetchSystemWaypointsQueryRequest : PaginatedIdentifiedQueryRequest<FetchSystemWaypointsQueryRequest, FetchSystemWaypointsQueryResponse>
{
    public string SystemSymbol { get; set; } = string.Empty;
}
