namespace mark.davison.spacetraders.core.Entities;

public class Spaceship : SpaceEntity
{
    public string Symbol { get; set; } = string.Empty;
    public SpaceshipState GlobalState { get; set; }
    public ShipRole ShipRole { get; set; }
    public DateTime? CurrentActionCompleteTime { get; set; }

    public Guid? ContractId { get; set; }

    public virtual SpaceContract? Contract { get; set; }
}
