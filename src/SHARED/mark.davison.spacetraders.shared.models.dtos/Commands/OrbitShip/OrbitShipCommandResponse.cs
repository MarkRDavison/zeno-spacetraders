namespace mark.davison.spacetraders.shared.models.dtos.Commands.OrbitShip;

public sealed class OrbitShipCommandResponse : Response<ShipNavDto>
{
    public string ShipSymbol { get; set; } = string.Empty;
}
