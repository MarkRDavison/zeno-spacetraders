namespace mark.davison.spacetraders.web.components;

public sealed class Routes
{
    public const string Root = "/";
    public const string Dashboard = Root;
    public const string Ships = "/ships";
    public const string Contracts = "/contracts";
    public const string Waypoints = "/systems/{systemSymbol}/waypoints";
    public const string Waypoint = "/systems/{systemSymbol}/waypoints/{waypointSymbol}";
}

public sealed class RouteHelpers
{
    public static string Waypoint(string systemSymbol, string waypointSymbol) => Routes.Waypoint.Replace("{systemSymbol}", systemSymbol).Replace("{waypointSymbol}", waypointSymbol);
    public static string Waypoints(string systemSymbol) => Routes.Waypoints.Replace("{systemSymbol}", systemSymbol);
}