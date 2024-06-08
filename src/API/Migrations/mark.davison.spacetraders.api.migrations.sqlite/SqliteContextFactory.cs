namespace mark.davison.spacetraders.api.migrations.sqlite;

[ExcludeFromCodeCoverage]
[DatabaseMigrationAssembly(DatabaseType.Sqlite)]
public sealed class SqliteContextFactory : SqliteDbContextFactory<SpacetradersDbContext>
{
    protected override SpacetradersDbContext DbContextCreation(
            DbContextOptions<SpacetradersDbContext> options
        ) => new SpacetradersDbContext(options);
}
