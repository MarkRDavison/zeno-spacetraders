namespace mark.davison.spacetraders.shared.models.dtos.Shared;

public sealed class ShipResponse
{
    public required ShipDto? Ship { get; init; }
    public required ShipNavDto? ShipNav { get; init; }
    public required ShipNavRouteDto? ShipNavRoute { get; init; }
    public required ShipCargoDto? ShipCargo { get; init; }
    public required CooldownDto? Cooldown { get; init; }
    public required ShipFuelDto? Fuel { get; init; }
}
