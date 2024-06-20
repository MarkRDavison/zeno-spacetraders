namespace mark.davison.spacetraders.shared.models.dtos.Commands.PurchaseShip;

[PostRequest(Path = "purchase-ship-command")]
public sealed class PurchaseShipCommandRequest : IdentifiedCommandRequest<PurchaseShipCommandRequest, PurchaseShipCommandResponse>
{
    public string WaypointSymbol { get; set; } = string.Empty;
    public string ShipType { get; set; } = string.Empty;
}