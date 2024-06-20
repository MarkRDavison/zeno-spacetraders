namespace mark.davison.spacetraders.shared.models.Helpers;

public static class WaypointHelpers
{
    public static WaypointResponse ToWaypointResponse(Waypoint? waypoint) => ToWaypointResponse(waypoint, null, null);
    public static WaypointResponse ToWaypointResponse(Shipyard? shipyard) => ToWaypointResponse(null, shipyard, null);
    public static WaypointResponse ToWaypointResponse(Market? marketplace) => ToWaypointResponse(null, null, marketplace);
    private static WaypointResponse ToWaypointResponse(Waypoint? waypoint, Shipyard? shipyard, Market? marketplace)
    {
        return new WaypointResponse
        {
            Waypoint = waypoint is null ? null : FromWaypoint(waypoint),
            Shipyard = shipyard is null ? null : FromShipyard(shipyard),
            Marketplace = marketplace is null ? null : FromMarketplace(marketplace)
        };
    }

    public static MarketplaceDto FromMarketplace(Market marketplace)
    {
        return new MarketplaceDto
        {
            WaypointSymbol = marketplace.Symbol,
            Exports = marketplace.Exports is null ? [] : [.. marketplace.Exports.Select(FromTradeGood)],
            Imports = marketplace.Imports is null ? [] : [.. marketplace.Imports.Select(FromTradeGood)],
            TradeGoods = marketplace.TradeGoods is null ? [] : [.. marketplace.TradeGoods.Select(FromMarketTradeGood)]
        };
    }

    public static TradeGoodDto FromTradeGood(TradeGood good)
    {
        return new TradeGoodDto
        {
            TradeSymbol = good.Symbol.ToString(),
            Name = good.Name,
            Description = good.Description
        };
    }

    public static MarketTradeGoodDto FromMarketTradeGood(MarketTradeGood good)
    {
        return new MarketTradeGoodDto
        {
            Type = good.Type.ToString(),
            TradeSymbol = good.Symbol.ToString(),
            TradeVolume = good.TradeVolume,
            Activity = good.Activity.ToString(),
            Supply = good.Supply.ToString(),
            PurchasePrice = good.PurchasePrice,
            SellPrice = good.SellPrice
        };
    }

    public static WaypointDto FromWaypoint(Waypoint waypoint)
    {
        return new WaypointDto
        {
            WaypointSymbol = waypoint.Symbol,
            SystemSymbol = waypoint.SystemSymbol,
            Type = waypoint.Type.ToString(),
            X = waypoint.X,
            Y = waypoint.Y,
            Traits = [.. waypoint.Traits.Select(_ => _.Name)]
        };
    }

    public static ShipyardDto FromShipyard(Shipyard shipyard)
    {
        return new ShipyardDto
        {
            WaypointSymbol = shipyard.Symbol,
            ModificationsFee = shipyard.ModificationsFee,
            ShipTypes = shipyard.ShipTypes == null ? [] : [.. shipyard.ShipTypes.Select(_ => _.Type.ToString())],
            ShipyardShips = shipyard.Ships == null ? [] : [.. shipyard.Ships.Select(FromShipyardShip)],
            ShipyardTransactions = shipyard.Transactions == null ? [] : [.. shipyard.Transactions.Select(FromShipyardTransaction)]
        };
    }

    public static ShipyardShipDto FromShipyardShip(ShipyardShip shipyardShip)
    {
        return new ShipyardShipDto
        {
            Type = shipyardShip.Type.ToString(),
            Name = shipyardShip.Name,
            Description = shipyardShip.Description,
            PurchasePrice = shipyardShip.PurchasePrice,
            Supply = shipyardShip.Supply.ToString()
        };
    }

    public static ShipyardTransactionDto FromShipyardTransaction(ShipyardTransaction shipyardTransaction)
    {
        return new ShipyardTransactionDto
        {
            Type = shipyardTransaction.ShipType.ToString(),
            AgentSymbol = shipyardTransaction.AgentSymbol,
            Price = shipyardTransaction.Price
        };
    }
}
