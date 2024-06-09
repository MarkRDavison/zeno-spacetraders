namespace mark.davison.spacetraders.shared.models.dtos.Commands.OrbitShip;

[PostRequest(Path = "orbit-ship-command")]
public sealed class OrbitShipCommandRequest : ICommand<OrbitShipCommandRequest, OrbitShipCommandResponse>
{
    public Guid AccountId { get; set; }
    public string ShipSymbol { get; set; } = string.Empty;
}
