namespace mark.davison.spacetraders.console.Procedures;

public static class QueryShips
{
    public static async Task<List<QueryShipInfo>> QueryShipInfo(SpacetradersDbContext dbContext, SpaceTradersApiClient api, bool remote = false)
    {
        var localShips = await dbContext
            .Set<Spaceship>()
            .AsNoTracking()
            .Select(_ => new QueryShipInfo(_.Id, _.Symbol, _.ShipRole, _.GlobalState, _.ContractId))
            .ToListAsync();

        if (remote)
        {

        }

        return localShips;
    }
}
