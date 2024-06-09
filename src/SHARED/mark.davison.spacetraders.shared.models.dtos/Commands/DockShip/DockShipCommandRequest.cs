namespace mark.davison.spacetraders.shared.models.dtos.Commands.DockShip;

[PostRequest(Path = "dock-ship-command")]
public sealed class DockShipCommandRequest : ICommand<DockShipCommandRequest, DockShipCommandResponse>
{
    public Guid AccountId { get; set; }
    public string ShipSymbol { get; set; } = string.Empty;
}
