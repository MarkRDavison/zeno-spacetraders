namespace mark.davison.spacetraders.web.features.Store.ShipUseCase;

public sealed class FetchShipsAction : BaseAction
{
    public Guid AccountId { get; set; }
    public MetaInfo Meta { get; set; } = new();
}

public sealed class FetchShipsActionResponse : BaseActionResponse<List<ShipDto>>
{

}

public sealed class PurchaseShipAction : BaseAction
{
    public Guid AccountId { get; set; }
    public string WaypointSymbol { get; set; } = string.Empty;
    public string ShipType { get; set; } = string.Empty;
}

public sealed class PurchaseShipActionResponse : BaseActionResponse<ShipDto>
{

}

public sealed class FetchShipAction : BaseAction
{
    public Guid AccountId { get; set; }
    public string ShipSymbol { get; set; } = string.Empty;
}

public sealed class FetchShipActionResponse : BaseActionResponse<ShipDto>
{

}