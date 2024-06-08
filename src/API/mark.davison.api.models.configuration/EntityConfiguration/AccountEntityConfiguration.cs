namespace mark.davison.api.models.configuration.EntityConfiguration;

public sealed class AccountEntityConfiguration : BaseEntityConfiguration<Account>
{
    public override void ConfigureEntity(EntityTypeBuilder<Account> builder)
    {
        builder.Property(_ => _.Token);
        builder.Property(_ => _.Identifier);
        builder.Property(_ => _.Version);
        builder.Property(_ => _.Email);
    }
}
