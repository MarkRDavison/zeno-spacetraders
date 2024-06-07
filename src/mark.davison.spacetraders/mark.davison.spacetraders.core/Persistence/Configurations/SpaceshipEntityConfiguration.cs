namespace mark.davison.spacetraders.core.Persistence.Configurations;

public class SpaceshipEntityConfiguration : SpaceEntityConfiguration<Spaceship>
{
    public override void ConfigureEntity(EntityTypeBuilder<Spaceship> builder)
    {
        builder.Property(_ => _.Symbol);
        builder.Property(_ => _.GlobalState).HasConversion<string>();
        builder.Property(_ => _.ShipRole).HasConversion<int>();
        builder.Property(_ => _.CurrentActionCompleteTime);
    }
}
