namespace mark.davison.spacetraders.shared.client.Adapters;

public static class ShipAdapters
{
    public static ShipResponse AdaptResponse(this Ship ship)
    {
        return new ShipResponse
        {
            Ship = ship.Adapt()
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
}
