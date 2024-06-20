namespace mark.davison.spacetraders.shared.models.Helpers;

public static class ShipHelpers
{
    public static ShipResponse ToShipResponse(Ship ship)
    {
        return new ShipResponse
        {
            Ship = ToShipDto(ship),
            ShipNav = ToShipNavDto(ship.Symbol, ship.Nav),
            ShipNavRoute = ToShipNavRouteDto(ship.Symbol, ship.Nav.Route),
            ShipCargo = ToShipCargoDto(ship.Symbol, ship.Cargo),
            Cooldown = ToCooldownDto(ship.Symbol, ship.Cooldown),
            Fuel = ToFuelDto(ship.Symbol, ship.Fuel)
        };
    }

    public static ShipFuelDto ToFuelDto(string symbol, ShipFuel fuel)
    {
        return new ShipFuelDto
        {
            ShipSymbol = symbol,
            Capacity = fuel.Capacity,
            Current = fuel.Current
        };
    }

    public static ShipDto ToShipDto(Ship ship)
    {
        return new ShipDto
        {
            ShipSymbol = ship.Symbol,
            Role = ship.Registration.Role.ToString()
        };
    }

    public static ShipNavDto ToShipNavDto(string shipSymbol, ShipNav shipNav)
    {
        return new ShipNavDto
        {
            ShipSymbol = shipSymbol,
            FlightMode = shipNav.FlightMode.ToString(),
            Status = shipNav.Status.ToString(),
            SystemSymbol = shipNav.SystemSymbol,
            WaypointSymbol = shipNav.WaypointSymbol
        };
    }

    public static ShipNavRouteDto ToShipNavRouteDto(string shipSymbol, ShipNavRoute shipNavRoute)
    {
        return new ShipNavRouteDto
        {
            ShipSymbol = shipSymbol,
            Arrival = shipNavRoute.Arrival,
            Departure = shipNavRoute.DepartureTime,
            Destination = shipNavRoute.Destination.Symbol,
            DestinationType = shipNavRoute.Destination.Type.ToString(),
            Origin = shipNavRoute.Origin.Symbol,
            OriginType = shipNavRoute.Origin.Type.ToString()
        };
    }

    public static ShipCargoDto ToShipCargoDto(string shipSymbol, ShipCargo shipCargo)
    {
        return new ShipCargoDto
        {
            ShipSymbol = shipSymbol,
            Capacity = shipCargo.Capacity,
            Units = shipCargo.Units,
            Inventory = [.. shipCargo.Inventory.Select(ToShipCargoItemDto)]
        };
    }

    public static CooldownDto ToCooldownDto(string shipSymbol, Cooldown cooldown)
    {
        return new CooldownDto
        {
            ShipSymbol = shipSymbol,
            TotalSeconds = cooldown.TotalSeconds,
            RemainingSeconds = cooldown.RemainingSeconds,
            Expiration = cooldown.Expiration
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
