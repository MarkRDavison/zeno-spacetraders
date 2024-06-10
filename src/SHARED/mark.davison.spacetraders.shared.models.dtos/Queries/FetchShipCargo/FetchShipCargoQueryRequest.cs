namespace mark.davison.spacetraders.shared.models.dtos.Queries.FetchShipCargo;

[GetRequest(Path = "fetch-ship-cargo")]
public sealed class FetchShipCargoQueryRequest : IQuery<FetchShipCargoQueryRequest, FetchShipCargoQueryResponse>
{
    public Guid AccountId { get; set; }
    public string ShipSymbol { get; set; } = string.Empty;
}
