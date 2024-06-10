namespace mark.davison.spacetraders.shared.models.dtos.Commands.RefuelShip;

public sealed class RefuelShipCommandResponse : Response<ShipFuelDto>
{
    public long Credits { get; set; }
}
