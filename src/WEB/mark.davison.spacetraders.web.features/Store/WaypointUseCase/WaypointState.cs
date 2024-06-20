namespace mark.davison.spacetraders.web.features.Store.WaypointUseCase;

[FeatureState]
public sealed class WaypointState
{
    public bool IsLoading { get; }
    public ReadOnlyCollection<WaypointDto> Waypoints { get; }
    public ReadOnlyCollection<ShipyardDto> Shipyards { get; }
    public ReadOnlyCollection<MarketplaceDto> Marketplaces { get; }

    public WaypointState() : this(false, [], [], [])
    {

    }

    public WaypointState(
        bool isLoading,
        IEnumerable<WaypointDto> waypoints,
        IEnumerable<ShipyardDto> shipyards,
        IEnumerable<MarketplaceDto> marketplaces)
    {
        IsLoading = isLoading;
        Waypoints = new ReadOnlyCollection<WaypointDto>(waypoints.ToList());
        Shipyards = new ReadOnlyCollection<ShipyardDto>(shipyards.ToList());
        Marketplaces = new ReadOnlyCollection<MarketplaceDto>(marketplaces.ToList());
    }

    public WaypointDto? GetWaypoint(string waypointSymbol) => Waypoints.FirstOrDefault(_ => _.WaypointSymbol == waypointSymbol);
    public ShipyardDto? GetShipyard(string waypointSymbol) => Shipyards.FirstOrDefault(_ => _.WaypointSymbol == waypointSymbol);
    public MarketplaceDto? GetMarketplace(string waypointSymbol) => Marketplaces.FirstOrDefault(_ => _.WaypointSymbol == waypointSymbol);
}
