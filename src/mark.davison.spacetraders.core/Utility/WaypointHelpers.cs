namespace mark.davison.spacetraders.core.Utility;

public static class WaypointsHelpers
{
    public static string GetSystemFromWaypoint(this string waypoint)
    {
        var splits = waypoint.Split("-");
        if (splits.Length != 3) { throw new InvalidDataException("Waypoint is in an invalid format: " + waypoint); }

        return $"{splits[0]}-{splits[1]}";
    }
}
