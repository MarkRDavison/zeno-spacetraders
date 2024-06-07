namespace mark.davison.spacetraders.core.Persistence;

public class SpacetradersDbContext : DbContext
{
    public SpacetradersDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SpaceshipEntityConfiguration).Assembly);
    }

    public DbSet<Spaceship> SpaceShips => Set<Spaceship>();
    public DbSet<SpaceContract> Contracts => Set<SpaceContract>();
    public DbSet<SpaceAgent> Agents => Set<SpaceAgent>();
}
