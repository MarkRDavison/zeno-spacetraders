namespace mark.davison.spacetraders.web.components.Pages.Ship;

public partial class ShipNavigation
{
    private bool _inProgress;

    [Parameter, EditorRequired]
    public required string Identifier { get; set; }

    [Parameter, EditorRequired]
    public required ShipDto Ship { get; set; }

    [Parameter, EditorRequired]

    public required ShipNavDto ShipNav { get; set; }

    [Parameter, EditorRequired]
    public required ShipNavRouteDto ShipNavRoute { get; set; }

    [Parameter, EditorRequired]
    public required WaypointDto CurrentWaypoint { get; set; }

    [Inject]
    public required IState<WaypointState> WaypointState { get; set; }

    [Inject]
    public required IState<ShipState> ShipState { get; set; }

    [Inject]
    public required IStoreHelper StoreHelper { get; set; }

    private string NavigationSystemText = string.Empty;

    private WaypointDto? NavigationDestination { get; set; }


    private async Task MoveShipToOrbit()
    {
        _inProgress = true; // TODO: Process monitor/activity monitor
        await StoreHelper.DispatchAndWaitForResponse<OrbitShipAction, UpdateShipsActionResponse>(new()
        {
            Identifier = Identifier,
            ShipSymbol = Ship.ShipSymbol
        });
        _inProgress = false;
    }

    private async Task DockShip()
    {
        _inProgress = true; // TODO: Process monitor/activity monitor
        await StoreHelper.DispatchAndWaitForResponse<DockShipAction, UpdateShipsActionResponse>(new()
        {
            Identifier = Identifier,
            ShipSymbol = Ship.ShipSymbol
        });
        _inProgress = false;
    }

    private async Task SearchDestination(string destination)
    {
        NavigationDestination = WaypointState.Value.Waypoints.FirstOrDefault(_ => _.WaypointSymbol == destination);

        if (NavigationDestination == null)
        {
            await StoreHelper.DispatchAndWaitForResponse<FetchWaypointAction, UpdateWaypointsActionResponse>(new()
            {
                Identifier = Identifier,
                SystemSymbol = WaypointsHelpers.GetSystemFromWaypoint(destination),
                WaypointSymbol = destination
            });

            NavigationDestination = WaypointState.Value.Waypoints.FirstOrDefault(_ => _.WaypointSymbol == destination);
        }
    }

    private double? Distance()
    {
        if (NavigationDestination is not null)
        {
            var sourceX = (double)CurrentWaypoint.X;
            var sourceY = (double)CurrentWaypoint.Y;
            var destX = (double)NavigationDestination.X;
            var destY = (double)NavigationDestination.Y;

            var distSquared = Math.Pow(Math.Abs(sourceX - destX), 2.0) + Math.Pow(Math.Abs(sourceY - destY), 2.0);

            return Math.Sqrt(distSquared);
        }

        return null;
    }

    private async Task GoToDestination(WaypointDto destination)
    {
        _inProgress = true;// TODO: IDisposable that wraps in progress?

        await StoreHelper.DispatchAndWaitForResponse<NavigateShipAction, UpdateShipsActionResponse>(new NavigateShipAction
        {
            Identifier = Identifier,
            ShipSymbol = Ship.ShipSymbol,
            DestinationWaypoint = destination.WaypointSymbol
        });

        NavigationSystemText = string.Empty;
        NavigationDestination = null;

        _inProgress = false;
    }
}
