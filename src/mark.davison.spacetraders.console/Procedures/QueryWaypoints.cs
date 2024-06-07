namespace mark.davison.spacetraders.console.Procedures;

public static class QueryWaypoints
{
    public static async Task<List<QuerySystemShipyardShipInfo>> QuerySystemShipyardShips(string system, SpaceTradersApiClient api, ShipType? type)
    {
        List<QuerySystemShipyardShipInfo> shipInfo = new();

        var shipyardWaypoints = await api.GetSystemWaypointsAsync(
            null,
            null,
            null,
            WaypointTraitSymbol.SHIPYARD,
            system);
        await Task.Delay(1000);

        foreach (var waypoint in shipyardWaypoints.Data)
        {
            var shipyard = await api.GetShipyardAsync(waypoint.SystemSymbol, waypoint.Symbol);
            await Task.Delay(1000);
            if (shipyard.Data.Ships != null)
            {
                foreach (var ship in shipyard.Data.Ships)
                {
                    if (type == null || ship.Type == type)
                    {
                        shipInfo.Add(new QuerySystemShipyardShipInfo(
                            ship.Type,
                            ship.PurchasePrice,
                            shipyard.Data.Symbol));
                    }
                }
            }
        }

        return shipInfo;
    }
}
