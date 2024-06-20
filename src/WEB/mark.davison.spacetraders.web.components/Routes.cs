namespace mark.davison.spacetraders.web.components;

public sealed class Routes
{
    public const string Root = "/";
    public const string Dashboard = Root;
    public const string Accounts = "/accounts";
    public const string Account = "/accounts/{identifier}";
    public const string Ships = "/accounts/{identifier}/ships";
    public const string Ship = "/accounts/{identifier}/ships/{shipSymbol}";
    public const string Contracts = "/accounts/{identifier}/contracts";
    public const string Contract = "/accounts/{identifier}/contracts/{contractId}";
    public const string Waypoints = "/accounts/{identifier}/systems/{systemSymbol}/waypoints";
    public const string Waypoint = "/accounts/{identifier}/systems/{systemSymbol}/waypoints/{waypointSymbol}";
}

public sealed class RouteHelpers
{
    public static string Account(string identifier) => Routes.Account.Replace("{identifier}", identifier);
    public static string Ships(string identifier) => Routes.Ships.Replace("{identifier}", identifier);
    public static string Ship(string identifier, string shipSymbol) => Routes.Ship.Replace("{identifier}", identifier).Replace("{shipSymbol}", shipSymbol);
    public static string Contracts(string identifier) => Routes.Contracts.Replace("{identifier}", identifier);
    public static string Contract(string identifier, string contractId) => Routes.Contract.Replace("{identifier}", identifier).Replace("{contractId}", contractId);
    public static string Waypoint(string identifier, string systemSymbol, string waypointSymbol) => Routes.Waypoint.Replace("{identifier}", identifier).Replace("{systemSymbol}", systemSymbol).Replace("{waypointSymbol}", waypointSymbol);
    public static string Waypoints(string identifier, string systemSymbol) => Routes.Waypoints.Replace("{identifier}", identifier).Replace("{systemSymbol}", systemSymbol);
}