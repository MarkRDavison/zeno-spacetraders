namespace mark.davison.spacetraders.web.features.Store.WaypointUseCase;

[FeatureState]
public sealed class WaypointState
{
    public bool IsLoading { get; }
    public ReadOnlyCollection<WaypointDto> Waypoints { get; }

    public WaypointState() : this(false, [])
    {

    }

    public WaypointState(bool isLoading, IEnumerable<WaypointDto> waypoints)
    {
        IsLoading = isLoading;
        Waypoints = new ReadOnlyCollection<WaypointDto>(waypoints.ToList());
    }
}
