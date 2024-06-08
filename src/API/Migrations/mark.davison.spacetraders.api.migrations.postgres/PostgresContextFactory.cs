namespace mark.davison.spacetraders.api.migrations.postgres;

[ExcludeFromCodeCoverage]
[DatabaseMigrationAssembly(DatabaseType.Postgres)]
public sealed class PostgresContextFactory : PostgresDbContextFactory<SpacetradersDbContext>
{
    protected override string ConfigName => "DATABASE";

    protected override SpacetradersDbContext DbContextCreation(
            DbContextOptions<SpacetradersDbContext> options
        ) => new SpacetradersDbContext(options);
}
