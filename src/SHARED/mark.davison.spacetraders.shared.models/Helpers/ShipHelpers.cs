namespace mark.davison.spacetraders.shared.models.Helpers;

public static class ShipHelpers
{
    public static ShipDto ToShipDto(Ship ship)
    {
        return new ShipDto
        {
            Symbol = ship.Symbol,
            Role = ship.Registration.Role.ToString(),
            SystemSymbol = ship.Nav.SystemSymbol,
            WaypointSymbol = ship.Nav.WaypointSymbol,
            ShipNavFlightMode = ship.Nav.FlightMode.ToString(),
            ShipNavStatus = ship.Nav.Status.ToString()
        };
    }
}
