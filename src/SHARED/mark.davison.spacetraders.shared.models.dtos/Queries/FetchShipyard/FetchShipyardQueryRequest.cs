namespace mark.davison.spacetraders.shared.models.dtos.Queries.FetchShipyard;

[GetRequest(Path = "fetch-shipyard-query")]
public sealed class FetchShipyardQueryRequest : IdentifiedQueryRequest<FetchShipyardQueryRequest, FetchShipyardQueryResponse>
{
    public string SystemSymbol { get; set; } = string.Empty;
    public string WaypointSymbol { get; set; } = string.Empty;
}
