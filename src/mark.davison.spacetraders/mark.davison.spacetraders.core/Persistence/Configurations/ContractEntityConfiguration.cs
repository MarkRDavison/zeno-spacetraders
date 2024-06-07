namespace mark.davison.spacetraders.core.Persistence.Configurations;

public class ContractEntityConfiguration : SpaceEntityConfiguration<SpaceContract>
{
    public override void ConfigureEntity(EntityTypeBuilder<SpaceContract> builder)
    {
        builder.Property(_ => _.ExternalId);
    }
}
