namespace mark.davison.spacetraders.shared.models.dtos.Queries.FetchShip;

[GetRequest(Path = "fetch-ship-query")]
public sealed class FetchShipQueryRequest : IdentifiedQueryRequest<FetchShipQueryRequest, FetchShipQueryResponse>
{
    public string ShipSymbol { get; set; } = string.Empty;
}
