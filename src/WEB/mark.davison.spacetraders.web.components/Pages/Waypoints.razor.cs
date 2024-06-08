namespace mark.davison.spacetraders.web.components.Pages;

public partial class Waypoints
{
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

    // TODO: Better way to do this, sharing enums, split core into types and api
    private List<string> Traits { get; } =
    [
        "MARKETPLACE",
        "SHIPYARD",
        "OUTPOST",
        "INDUSTRIAL",
        "MINERAL_DEPOSITS",
        "COMMON_METAL_DEPOSITS",
        "PRECIOUS_METAL_DEPOSITS",
        "RARE_METAL_DEPOSITS"
    ];

    private string? SelectedTrait { get; set; }


    protected override async Task OnInitializedAsync()
    {
        await LoadState();
    }

    private async Task Search()
    {
        await LoadState();
    }

    private async Task LoadState()
    {
        if (AccountContextService.GetActiveAccount() is { } account)
        {
            await StoreHelper.DispatchAndWaitForResponse<FetchWaypointsAction, FetchWaypointsActionResponse>(
                new FetchWaypointsAction
                {
                    AccountId = account.Id,
                    SystemSymbol = SystemSymbol,
                    Trait = SelectedTrait ?? string.Empty,
                    Meta = new MetaInfo
                    {
                        Limit = 20
                    }
                });
        }
    }
}
