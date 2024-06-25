namespace mark.davison.spacetraders.shared.models.dtos.Commands.ModifyShipState;

[PostRequest(Path = "modify-ship-state")]
public sealed class ModifyShipStateCommandRequest : IdentifiedCommandRequest<ModifyShipStateCommandRequest, ModifyShipStateCommandResponse>
{
    public string ShipSymbol { get; set; } = string.Empty;
    public bool SetOrbiting { get; set; }
    public bool SetDocked { get; set; }
}
