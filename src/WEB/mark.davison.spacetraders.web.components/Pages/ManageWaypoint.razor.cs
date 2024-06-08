namespace mark.davison.spacetraders.web.components.Pages;

public partial class ManageWaypoint
{
    [Parameter]
    public required string WaypointSymbol { get; set; }
    [Parameter]
    public required string SystemSymbol { get; set; }

    [Inject]
    public required IState<WaypointState> WaypointState { get; set; }

    [Inject]
    public required IState<AccountState> AccountState { get; set; }

    [Inject]
    public required IStoreHelper StoreHelper { get; set; }

    [Inject]
    public required IAccountContextService AccountContextService { get; set; }

    public WaypointDto? WaypointDto => WaypointState.Value.Waypoints.FirstOrDefault(_ => _.WaypointSymbol == WaypointSymbol);

    public ShipyardDto? ShipyardDto => WaypointState.Value.Shipyards.FirstOrDefault(_ => _.Symbol == WaypointSymbol);

    public bool IsShipyard => WaypointDto?.Traits.Any(_ => _.ToUpper() == "SHIPYARD") ?? false;

    protected override async Task OnParametersSetAsync()
    {
        if (WaypointDto == null && !string.IsNullOrEmpty(SystemSymbol) && !string.IsNullOrEmpty(WaypointSymbol))
        {
            // TODO: Load symbol
            await Task.CompletedTask;
        }
    }

    private async void IsExpandedChange(bool expanded, string trait)
    {
        if (!expanded) return;

        if (AccountContextService.GetActiveAccount() is { } account) // TODO: Better way of getting account id
        {
            if (trait == "Shipyard" && ShipyardDto == null)
            {
                await StoreHelper.DispatchAndWaitForResponse<FetchShipyardAction, FetchShipyardActionResponse>(new FetchShipyardAction
                {
                    AccountId = account.Id,
                    SystemSymbol = SystemSymbol,
                    WaypointSymbol = WaypointSymbol
                });
            }
        }
    }
}
