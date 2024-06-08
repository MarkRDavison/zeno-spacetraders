namespace mark.davison.spacetraders.shared.constants;
public static class WaypointHelpers
{
    public static string GetSystemFromWaypoint(this string waypoint)
    {
        var splits = waypoint.Split("-");
        if (splits.Length != 3) { throw new InvalidDataException("Waypoint is in an invalid format: " + waypoint); }

        return $"{splits[0]}-{splits[1]}";
    }
}
