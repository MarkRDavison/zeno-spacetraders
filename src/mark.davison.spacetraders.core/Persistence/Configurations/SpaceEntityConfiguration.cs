namespace mark.davison.spacetraders.core.Persistence.Configurations;

public abstract class SpaceEntityConfiguration<T> : IEntityTypeConfiguration<T>
    where T : SpaceEntity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder
            .HasKey(e => e.Id);

        builder
            .Property(e => e.Id)
            .ValueGeneratedNever();

        NavigationPropertyEntityConfigurations.ConfigureEntity(builder);
        ConfigureEntity(builder);
    }

    public abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
}