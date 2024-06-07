namespace mark.davison.spacetraders.avalonia.ui.ViewModels.Authenticated;

public partial class WaypointsInfoPageViewModel : AsyncViewModel
{
    private readonly IWaypointService _waypointService;
    private readonly IApplicationNotificationService _applicationNotificationService;
    private readonly IAgentInfoPageViewModelService _agentInfoPageViewModelService;

    public WaypointsInfoPageViewModel(
        IWaypointService waypointService,
        IApplicationNotificationService applicationNotificationService,
        IAgentInfoPageViewModelService agentInfoPageViewModelService)
    {
        _waypointService = waypointService;
        _applicationNotificationService = applicationNotificationService;
        _agentInfoPageViewModelService = agentInfoPageViewModelService;
    }

    protected override async Task InitializeAsync()
    {
        await FetchWaypoints(WaypointTraitSymbol);
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PageProgress))]
    [NotifyCanExecuteChangedFor(nameof(AdjustPageCommand))]
    private ObservableCollection<WaypointInfoModel> _waypoints = [];

    [ObservableProperty]
    private WaypointTraitSymbol? _waypointTraitSymbol;

    [ObservableProperty]
    private int _pageIndex = 1;

    private Meta _meta = new()
    {
        Limit = 20
    };

    private int MaxPageIndex => (int)Math.Ceiling((decimal)_meta.Total / (decimal)_meta.Limit);

    [RelayCommand(CanExecute = nameof(CanAdjustPage))]
    private async Task AdjustPage(int offset, CancellationToken cancellationToken)
    {
        var newPageIndex = PageIndex;
        if (offset < 0)
        {
            if (offset == -1)
            {
                newPageIndex--;
            }
            else
            {

                newPageIndex = 1;
            }
        }
        else
        {
            if (offset == 1)
            {
                newPageIndex++;
            }
            else
            {

                newPageIndex = MaxPageIndex;
            }
        }

        PageIndex = newPageIndex;
        await FetchWaypoints(WaypointTraitSymbol);
    }

    public string PageProgress => $"{PageIndex} / {Math.Max(MaxPageIndex, 1)}";

    private bool CanAdjustPage(int offset)
    {
        if (offset > 0)
        {
            return (PageIndex) < MaxPageIndex;
        }
        else
        {
            return (PageIndex) > 1;
        }
    }

    private async Task FetchWaypoints(WaypointTraitSymbol? traits)
    {
        var agent = await _agentInfoPageViewModelService.GetAgentAsync(CancellationToken.None);

        var systemWaypoint = WaypointHelpers.GetSystemFromWaypoint(agent.Headquarters);

        var response = await _waypointService.GetSystemWaypointsAsync(
            PageIndex,
            _meta.Limit,
            systemWaypoint,
            traits,
            CancellationToken.None);

        _meta = response.Meta;

        Dispatcher.UIThread.Invoke(() =>
        {
            Waypoints = [.. response.Waypoints];
        });
    }

}
