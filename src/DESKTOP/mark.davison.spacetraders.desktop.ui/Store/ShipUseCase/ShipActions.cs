namespace mark.davison.spacetraders.desktop.ui.Store.ShipUseCase;

public sealed class FetchShipsAction : IdentifiedBaseAction;

public sealed class ModifyShipStateAction : IdentifiedBaseAction
{
    public string ShipSymbol { get; set; } = string.Empty;
    public bool SetOrbiting { get; set; }
    public bool SetDocked { get; set; }
}

public sealed class UpdateShipsActionResponse : BaseActionResponse<List<ShipResponse>>;