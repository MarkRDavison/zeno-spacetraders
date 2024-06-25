namespace mark.davison.spacetraders.shared.client.Adapters;

public static class ShipAdapters
{
    public static ShipResponse AdaptResponse(this Ship ship)
    {
        return new ShipResponse
        {
            Ship = ship.Adapt(),
            ShipNav = ship.Nav.Adapt(ship.Symbol)
        };
    }

    public static ShipDto Adapt(this Ship ship)
    {
        return new ShipDto
        {
            Symbol = ship.Symbol,
            Role = ship.Registration.Role.ToString()
        };
    }

    public static ShipNavDto Adapt(this ShipNav shipNav, string shipSymbol)
    {
        return new ShipNavDto
        {
            ShipSymbol = shipSymbol,
            SystemSymbol = shipNav.SystemSymbol,
            WaypointSymbol = shipNav.WaypointSymbol,
            FlightMode = shipNav.FlightMode.ToString(),
            Status = shipNav.Status.ToString()
        };
    }
}
