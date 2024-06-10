namespace mark.davison.spacetraders.shared.models.dtos.Shared;

public sealed class ShipNavRouteDto
{
    public string Destination { get; set; } = string.Empty;
    public string DestinationType { get; set; } = string.Empty;
    public string Origin { get; set; } = string.Empty;
    public string OriginType { get; set; } = string.Empty;

    public DateTimeOffset Departure { get; set; }
    public DateTimeOffset Arrival { get; set; }
}
