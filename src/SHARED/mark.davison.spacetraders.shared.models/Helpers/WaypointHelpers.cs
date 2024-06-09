namespace mark.davison.spacetraders.shared.models.Helpers;

public static class WaypointHelpers
{
    public static WaypointDto FromWaypoint(Waypoint waypoint)
    {
        return new WaypointDto
        {
            WaypointSymbol = waypoint.Symbol,
            SystemSymbol = waypoint.SystemSymbol,
            Type = waypoint.Type.ToString(),
            Traits = [.. waypoint.Traits.Select(_ => _.Name)]
        };
    }
}
