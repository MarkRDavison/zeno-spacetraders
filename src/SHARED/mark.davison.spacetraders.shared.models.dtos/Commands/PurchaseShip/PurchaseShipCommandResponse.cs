namespace mark.davison.spacetraders.shared.models.dtos.Commands.PurchaseShip;

public sealed class PurchaseShipCommandResponse : Response<ShipDto>
{
    public long Credits { get; set; }
}
