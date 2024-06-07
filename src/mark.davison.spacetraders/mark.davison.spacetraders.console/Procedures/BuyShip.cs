namespace mark.davison.spacetraders.console.Procedures;

public static class BuyShip
{
    public static async Task<BuyShipResponse> PurchaseShip(
        SpacetradersDbContext dbContext,
        SpaceTradersApiClient api,
        ShipType type,
        string waypoint)
    {
        var shipPurchase = await api.PurchaseShipAsync(new Body4
        {
            ShipType = type,
            WaypointSymbol = waypoint
        });

        var ship = new Spaceship
        {
            Id = Guid.NewGuid(),
            GlobalState = SpaceshipState.IDLE,
            ShipRole = shipPurchase.Data.Ship.Registration.Role,
            Symbol = shipPurchase.Data.Ship.Symbol
        };
        await dbContext.AddAsync(ship);
        await dbContext.SaveChangesAsync();

        return new BuyShipResponse(ship.Symbol);
    }

    public static async Task<BuyShipResponse> PurchaseShipByTypeFromSystem(
        SpacetradersDbContext dbContext,
        SpaceTradersApiClient api,
        ShipType type,
        string system)
    {
        var shipyardShipInfo = await QueryWaypoints.QuerySystemShipyardShips(system, api, type);
        var agentInfo = await QueryAgent.QueryAgentInfo(api);

        var shipToPurchase = shipyardShipInfo
            .Where(_ => _.Price <= agentInfo.Credits)
            .OrderByDescending(_ => _.Price)
            .FirstOrDefault();

        if (shipToPurchase == null)
        {
            Console.Error.WriteLine("Cannot find ship to purchase");
            return new BuyShipResponse(string.Empty);
        }

        return await PurchaseShip(
            dbContext,
            api,
            shipToPurchase.Type,
            shipToPurchase.ShipyardWaypoint);
    }
}
