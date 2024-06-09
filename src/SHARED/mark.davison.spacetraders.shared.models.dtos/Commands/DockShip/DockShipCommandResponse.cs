namespace mark.davison.spacetraders.shared.models.dtos.Commands.DockShip;

public sealed class DockShipCommandResponse : Response<ShipNavDto>
{
    public string ShipSymbol { get; set; } = string.Empty;
}
