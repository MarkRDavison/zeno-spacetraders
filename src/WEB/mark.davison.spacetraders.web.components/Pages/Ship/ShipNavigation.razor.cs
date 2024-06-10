namespace mark.davison.spacetraders.web.components.Pages.Ship;

public partial class ShipNavigation
{
    private bool _inProgress;

    [Parameter, EditorRequired]
    public required ShipDto ShipDto { get; set; }

    [Inject]
    public required IState<WaypointState> WaypointState { get; set; }

    [Inject]
    public required IState<ShipState> ShipState { get; set; }

    [Inject]
    public required IAccountContextService AccountContextService { get; set; }

    [Inject]
    public required IStoreHelper StoreHelper { get; set; }

    private string NavigationSystemText = string.Empty;

    private WaypointDto? NavigationDestination { get; set; }

    private async Task MoveShipToOrbit()
    {
        if (AccountContextService.GetActiveAccount() is { } account)
        {
            _inProgress = true; // TODO: IDisposable that wraps in progress?
            await StoreHelper.DispatchAndWaitForResponse<OrbitShipAction, UpdateShipNavResponse>(new()
            {
                AccountId = account.Id,
                ShipSymbol = ShipDto.Symbol
            });
            _inProgress = false;
        }
    }

    private async Task DockShip()
    {
        if (AccountContextService.GetActiveAccount() is { } account)
        {
            _inProgress = true;// TODO: IDisposable that wraps in progress?
            await StoreHelper.DispatchAndWaitForResponse<DockShipAction, UpdateShipNavResponse>(new()
            {
                AccountId = account.Id,
                ShipSymbol = ShipDto.Symbol
            });
            _inProgress = false;
        }
    }

    private async Task SearchDestination(string destination)
    {
        if (AccountContextService.GetActiveAccount() is { } account)
        {
            NavigationDestination = WaypointState.Value.Waypoints.FirstOrDefault(_ => _.WaypointSymbol == destination);

            if (NavigationDestination == null)
            {
                await StoreHelper.DispatchAndWaitForResponse<FetchWaypointAction, FetchWaypointActionResponse>(new()
                {
                    AccountId = account.Id,
                    WaypointSymbol = destination
                });

                NavigationDestination = WaypointState.Value.Waypoints.FirstOrDefault(_ => _.WaypointSymbol == destination);
            }
        }
    }

    private async Task GoToDestination(WaypointDto destination)
    {
        if (AccountContextService.GetActiveAccount() is { } account)
        {
            _inProgress = true;// TODO: IDisposable that wraps in progress?

            await StoreHelper.DispatchAndWaitForResponse<NavigateShipAction, UpdateShipNavResponse>(new NavigateShipAction
            {
                AccountId = account.Id,
                ShipSymbol = ShipDto.Symbol,
                DestinationWaypoint = destination.WaypointSymbol
            });

            NavigationSystemText = string.Empty;
            NavigationDestination = null;
            _inProgress = false;
        }
    }
}
