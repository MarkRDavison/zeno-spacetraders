namespace mark.davison.spacetraders.web.features.Store.ShipUseCase;

public sealed class FetchShipsAction : PaginatedIdentifiedAction;

public sealed class FetchShipAction : IdentifiedAction
{
    public string ShipSymbol { get; set; } = string.Empty;
}

public sealed class OrbitShipAction : IdentifiedAction
{
    public string ShipSymbol { get; set; } = string.Empty;
}

public sealed class DockShipAction : IdentifiedAction
{
    public string ShipSymbol { get; set; } = string.Empty;
}

public sealed class PurchaseShipAction : IdentifiedAction
{
    public string WaypointSymbol { get; set; } = string.Empty;
    public string ShipType { get; set; } = string.Empty;
}

public sealed class ExtractResourcesAction : IdentifiedAction
{
    public string ShipSymbol { get; set; } = string.Empty;
}

public sealed class SellCargoAction : IdentifiedAction
{
    public string ShipSymbol { get; set; } = string.Empty;
    public string TradeSymbol { get; set; } = string.Empty;
    public int Units { get; set; }
}

public sealed class JettisonCargoAction : IdentifiedAction
{
    public string ShipSymbol { get; set; } = string.Empty;
    public string TradeSymbol { get; set; } = string.Empty;
    public int Units { get; set; }
}

public sealed class NavigateShipAction : IdentifiedAction
{
    public string ShipSymbol { get; set; } = string.Empty;
    public string DestinationWaypoint { get; set; } = string.Empty;
}

public sealed class DeliverContractCargoAction : IdentifiedAction
{
    public string ContractId { get; set; } = string.Empty;
    public string ShipSymbol { get; set; } = string.Empty;
    public string TradeSymbol { get; set; } = string.Empty;
    public int Units { get; set; }
}

public sealed class RefuelShipAction : IdentifiedAction
{
    public string ShipSymbol { get; set; } = string.Empty;
    public int Units { get; set; }
    public bool FromCargo { get; set; }
}

public sealed class UpdateShipsActionResponse : BaseActionResponse<List<ShipResponse>>;