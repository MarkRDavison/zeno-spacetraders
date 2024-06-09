namespace mark.davison.spacetraders.shared.models.Helpers;

public static class ShipHelpers
{
    public static ShipDto ToShipDto(Ship ship)
    {
        return new ShipDto
        {
            Symbol = ship.Symbol,
            Role = ship.Registration.Role.ToString(),
            Nav = ToShipNavDto(ship.Nav)
        };
    }

    public static ShipNavDto ToShipNavDto(ShipNav shipNav)
    {
        return new ShipNavDto
        {
            SystemSymbol = shipNav.SystemSymbol,
            WaypointSymbol = shipNav.WaypointSymbol,
            FlightMode = shipNav.FlightMode.ToString(),
            Status = shipNav.Status.ToString()
        };
    }
}
