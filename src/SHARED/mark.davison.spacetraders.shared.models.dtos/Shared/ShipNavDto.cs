namespace mark.davison.spacetraders.shared.models.dtos.Shared;

public sealed class ShipNavDto
{
    public string SystemSymbol { get; set; } = string.Empty;
    public string WaypointSymbol { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string FlightMode { get; set; } = string.Empty;
    public ShipNavRouteDto Route { get; set; } = new();
}