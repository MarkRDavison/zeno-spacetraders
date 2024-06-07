namespace mark.davison.spacetraders.core.Persistence.Configurations;

public class SpaceAgentEntityConfiguration : SpaceEntityConfiguration<SpaceAgent>
{
    public override void ConfigureEntity(EntityTypeBuilder<SpaceAgent> builder)
    {
        builder.Property(_ => _.Symbol);
        builder.Property(_ => _.AccountId);
        builder.Property(_ => _.Headquarters);
        builder.Property(_ => _.Token);
    }
}
