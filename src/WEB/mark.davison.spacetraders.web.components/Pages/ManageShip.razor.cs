namespace mark.davison.spacetraders.web.components.Pages;

public partial class ManageShip
{
    [Parameter]
    public required string ShipSymbol { get; set; }

    [Inject]
    public required IState<ShipState> ShipState { get; set; }

    [Inject]
    public required IState<WaypointState> WaypointState { get; set; }

    [Inject]
    public required IStoreHelper StoreHelper { get; set; }

    [Inject]
    public required IAccountContextService AccountContextService { get; set; }

    private ShipDto? ShipDto => ShipState.Value.Ships.FirstOrDefault(_ => _.Symbol == ShipSymbol);

    private WaypointDto? WaypointDto => WaypointState.Value.Waypoints.FirstOrDefault(_ => _.WaypointSymbol == ShipDto?.Nav.WaypointSymbol);

    protected override async Task OnParametersSetAsync()
    {
        if (AccountContextService.GetActiveAccount() is { } account)
        {
            bool fetchedShip = false;
            if (ShipDto == null && !string.IsNullOrEmpty(ShipSymbol))
            {
                await StoreHelper.DispatchAndWaitForResponse<FetchShipAction, FetchShipActionResponse>(new()
                {
                    AccountId = account.Id,
                    ShipSymbol = ShipSymbol
                });
                fetchedShip = true;
            }

            if (WaypointDto == null && ShipDto != null)
            {
                if (fetchedShip)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(500)); // TODO: Better throttling in the api
                }
                await StoreHelper.DispatchAndWaitForResponse<FetchWaypointAction, FetchWaypointActionResponse>(new()
                {
                    AccountId = account.Id,
                    WaypointSymbol = ShipDto.Nav.WaypointSymbol
                });
            }
        }
    }

    private async Task Refresh()
    {
        if (AccountContextService.GetActiveAccount() is { } account)
        {
            await StoreHelper.DispatchAndWaitForResponse<FetchShipAction, FetchShipActionResponse>(new()
            {
                AccountId = account.Id,
                ShipSymbol = ShipSymbol
            });

            await Task.Delay(TimeSpan.FromMilliseconds(500));

            await StoreHelper.DispatchAndWaitForResponse<FetchShipCargoAction, FetchShipCargoActionResponse>(new()
            {
                AccountId = account.Id,
                ShipSymbol = ShipSymbol
            });
        }
    }
    private async Task Extract()
    {

        if (AccountContextService.GetActiveAccount() is { } account)
        {
            await StoreHelper.DispatchAndWaitForResponse<ExtractResourceShipAction, FetchShipCargoActionResponse>(new()
            {
                AccountId = account.Id,
                ShipSymbol = ShipSymbol
            });
        }
    }

    private async Task Refuel()
    {
        if (ShipDto != null && AccountContextService.GetActiveAccount() is { } account)
        {
            await StoreHelper.DispatchAndWaitForResponse<RefuelShipAction, UpdateShipFuelResponse>(new()
            {
                AccountId = account.Id,
                ShipSymbol = ShipSymbol,
                Units = ShipDto.Fuel.Capacity - ShipDto.Fuel.Current
            });
        }
    }
}
