namespace mark.davison.spacetraders.api.persistence;

[ExcludeFromCodeCoverage]
public sealed class SpacetradersDbContext(DbContextOptions options) : DbContextBase<SpacetradersDbContext>(options), ISpacetradersDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEntityConfiguration).Assembly);
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Account> Accounts => Set<Account>();
}
