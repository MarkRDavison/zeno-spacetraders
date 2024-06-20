namespace mark.davison.spacetraders.web.components.Pages;

public partial class ManageWaypoint
{
    [Parameter]
    public required string Identifier { get; set; }

    [Parameter]
    public required string WaypointSymbol { get; set; }

    [Parameter]
    public required string SystemSymbol { get; set; }

    [Inject]
    public required IState<WaypointState> WaypointState { get; set; }

    [Inject]
    public required IStoreHelper StoreHelper { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (!string.IsNullOrEmpty(Identifier))
        {
            await StoreHelper.DispatchAndWaitForResponse<FetchContractsAction, UpdateContractsActionResponse>(new()
            {
                Identifier = Identifier
            });
        }
    }

    protected override async Task OnParametersSetAsync()
    {
        if (WaypointState.Value.GetWaypoint(WaypointSymbol) is null)
        {
            await StoreHelper.DispatchAndWaitForResponse<FetchWaypointAction, UpdateWaypointsActionResponse>(new()
            {
                Identifier = Identifier,
                SystemSymbol = SystemSymbol,
                WaypointSymbol = WaypointSymbol
            });
        }
    }

    private async Task OnExpandedChanged(bool expanded, string section)
    {
        if (!expanded) return;

        if (section == "SHIPYARD")
        {
            await StoreHelper.DispatchAndWaitForResponse<FetchShipyardAction, UpdateWaypointsActionResponse>(new()
            {
                Identifier = Identifier,
                SystemSymbol = SystemSymbol,
                WaypointSymbol = WaypointSymbol
            });
        }
        else if (section == "MARKETPLACE")
        {
            await StoreHelper.DispatchAndWaitForResponse<FetchMarketplaceAction, UpdateWaypointsActionResponse>(new()
            {
                Identifier = Identifier,
                SystemSymbol = SystemSymbol,
                WaypointSymbol = WaypointSymbol
            });
        }
    }

}
