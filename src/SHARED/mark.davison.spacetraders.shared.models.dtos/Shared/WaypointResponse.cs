namespace mark.davison.spacetraders.shared.models.dtos.Shared;

public sealed class WaypointResponse
{
    public required WaypointDto? Waypoint { get; init; }
    public required ShipyardDto? Shipyard { get; init; }
    public required MarketplaceDto? Marketplace { get; init; }
}
