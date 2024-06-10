namespace mark.davison.spacetraders.shared.models.dtos.Commands.NavigateShip;

[PostRequest(Path = "navigate-ship-command")]
public sealed class NavigateShipCommandRequest : ICommand<NavigateShipCommandRequest, NavigateShipCommandResponse>
{
    public Guid AccountId { get; set; }
    public string ShipSymbol { get; set; } = string.Empty;
    public string DestinationWaypoint { get; set; } = string.Empty;
}
