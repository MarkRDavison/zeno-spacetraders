using mark.davison.spacetraders.shared.constants;

namespace mark.davison.spacetraders.web.components.Pages;

public partial class Waypoints
{
    [Inject]
    public required IState<WaypointState> WaypointState { get; set; }

    [Inject]
    public required IState<AccountState> AccountState { get; set; }

    [Inject]
    public required IStoreHelper StoreHelper { get; set; }

    [Inject]
    public required IAccountContextService AccountContextService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await LoadState();
    }

    private async Task LoadState()
    {
        if (AccountContextService.GetActiveAccount() is { } account &&
            AccountState.Value.AccountSummaries.FirstOrDefault(_ => _.AccountId == account.Id) is { } summary)
        {
            await StoreHelper.DispatchAndWaitForResponse<FetchWaypointsAction, FetchWaypointsActionResponse>(
                new FetchWaypointsAction
                {
                    AccountId = account.Id,
                    SystemSymbol = WaypointHelpers.GetSystemFromWaypoint(summary.Headquarters),
                    Meta = new MetaInfo
                    {
                        Limit = 20
                    }
                });
        }
    }
}
