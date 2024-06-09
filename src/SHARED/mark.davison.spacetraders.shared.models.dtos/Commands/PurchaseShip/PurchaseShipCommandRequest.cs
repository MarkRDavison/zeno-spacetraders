namespace mark.davison.spacetraders.shared.models.dtos.Commands.PurchaseShip;

[PostRequest(Path = "purchase-ship-command")]
public sealed class PurchaseShipCommandRequest : ICommand<PurchaseShipCommandRequest, PurchaseShipCommandResponse>
{
    public Guid AccountId { get; set; }
    public string WaypointSymbol { get; set; } = string.Empty;
    public string ShipType { get; set; } = string.Empty;
}