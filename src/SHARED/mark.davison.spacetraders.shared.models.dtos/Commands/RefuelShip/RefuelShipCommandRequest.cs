namespace mark.davison.spacetraders.shared.models.dtos.Commands.RefuelShip;

[PostRequest(Path = "refuel-ship-command")]
public sealed class RefuelShipCommandRequest : IdentifiedCommandRequest<RefuelShipCommandRequest, RefuelShipCommandResponse>
{
    public string ShipSymbol { get; set; } = string.Empty;
    public bool FromCargo { get; set; }
    public int Units { get; set; }
}
