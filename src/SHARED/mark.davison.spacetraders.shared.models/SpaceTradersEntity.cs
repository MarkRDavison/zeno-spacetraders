namespace mark.davison.spacetraders.shared.models;

public class SpaceTradersEntity : BaseEntity
{
    public Guid UserId { get; set; }
    public virtual User? User { get; set; }
}