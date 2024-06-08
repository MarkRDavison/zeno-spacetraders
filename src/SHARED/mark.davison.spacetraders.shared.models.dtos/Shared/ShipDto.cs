namespace mark.davison.spacetraders.shared.models.dtos.Shared;

public sealed class ShipDto
{
    public string Symbol { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string SystemSymbol { get; set; } = string.Empty;
    public string WaypointSymbol { get; set; } = string.Empty;
    public string ShipNavStatus { get; set; } = string.Empty;
    public string ShipNavFlightMode { get; set; } = string.Empty;
}
