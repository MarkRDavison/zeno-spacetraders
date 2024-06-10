namespace mark.davison.spacetraders.shared.models.Helpers;

public static class ShipHelpers
{
    public static ShipDto ToShipDto(Ship ship)
    {
        return new ShipDto
        {
            Symbol = ship.Symbol,
            Role = ship.Registration.Role.ToString(),
            Nav = ToShipNavDto(ship.Nav),
            Fuel = ToShipFuelDto(ship.Fuel),
            Cooldown = ToCooldown(ship.Cooldown)
        };
    }

    private static CooldownDto ToCooldown(Cooldown cooldown)
    {
        return new CooldownDto
        {
            TotalSeconds = cooldown.TotalSeconds,
            RemainingSeconds = cooldown.RemainingSeconds,
            Expiration = cooldown.Expiration
        };
    }

    public static ShipNavDto ToShipNavDto(ShipNav shipNav)
    {
        return new ShipNavDto
        {
            SystemSymbol = shipNav.SystemSymbol,
            WaypointSymbol = shipNav.WaypointSymbol,
            FlightMode = shipNav.FlightMode.ToString(),
            Status = shipNav.Status.ToString(),
            Route = ToShipNavRouteDto(shipNav.Route)
        };
    }

    public static ShipFuelDto ToShipFuelDto(ShipFuel shipFuel)
    {
        return new ShipFuelDto
        {
            Capacity = shipFuel.Capacity,
            Current = shipFuel.Current
        };
    }

    public static ShipNavRouteDto ToShipNavRouteDto(ShipNavRoute shipNavRoute)
    {
        return new ShipNavRouteDto
        {
            Arrival = shipNavRoute.Arrival,
            Departure = shipNavRoute.DepartureTime,
            Destination = shipNavRoute.Destination.Symbol,
            DestinationType = shipNavRoute.Destination.Type.ToString(),
            Origin = shipNavRoute.Origin.Symbol,
            OriginType = shipNavRoute.Origin.Type.ToString()
        };
    }

    public static ShipCargoDto ToShipCargoDto(ShipCargo shipCargo)
    {
        return new ShipCargoDto
        {
            Capacity = shipCargo.Capacity,
            Units = shipCargo.Units,
            Inventory = [.. shipCargo.Inventory.Select(ToShipCargoItemDto)]
        };
    }

    public static ShipCargoItemDto ToShipCargoItemDto(ShipCargoItem shipCargoItem)
    {
        return new ShipCargoItemDto
        {
            Symbol = shipCargoItem.Symbol.ToString(),
            Name = shipCargoItem.Name,
            Description = shipCargoItem.Description,
            Units = shipCargoItem.Units
        };
    }
}
