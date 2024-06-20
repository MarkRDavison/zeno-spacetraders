namespace mark.davison.spacetraders.shared.models.dtos.Commands.NavigateShip;

[PostRequest(Path = "navigate-ship-command")]
public sealed class NavigateShipCommandRequest : IdentifiedCommandRequest<NavigateShipCommandRequest, NavigateShipCommandResponse>
{
    public string ShipSymbol { get; set; } = string.Empty;
    public string DestinationWaypoint { get; set; } = string.Empty;
}
