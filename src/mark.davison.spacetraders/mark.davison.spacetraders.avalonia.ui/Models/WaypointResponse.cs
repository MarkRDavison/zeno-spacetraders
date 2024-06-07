namespace mark.davison.spacetraders.avalonia.ui.Models;

public sealed class WaypointResponse
{
    public WaypointResponse(Meta meta, IEnumerable<Waypoint> waypoints)
    {
        Meta = meta;
        Waypoints = [.. waypoints.Select(_ => new WaypointInfoModel(_))];
    }

    public Meta Meta { get; }
    public List<WaypointInfoModel> Waypoints { get; }
}
