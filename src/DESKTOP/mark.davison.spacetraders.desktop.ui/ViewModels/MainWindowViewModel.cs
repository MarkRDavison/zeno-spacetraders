namespace mark.davison.spacetraders.desktop.ui.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly ICommonApplicationNotificationService _commonApplicationNotificationService;
    public MainWindowViewModel(IServiceProvider services)
    {
        _commonApplicationNotificationService = services.GetRequiredService<ICommonApplicationNotificationService>();

        BasicApplicationViewModel = new BasicApplicationViewModel(
            "Spacetraders",
            services)
        {
            AppBarChildContentViewModel = services.GetRequiredService<AgentSummaryViewModel>()
        };

        BasicApplicationViewModel.PageGroups.Add(new(
            "Accounts",
            [services.GetRequiredService<AccountsPageViewModel>()],
            PageGroupConstants.AccountGroupId));

        BasicApplicationViewModel.PageGroups.Add(new(
            "Contracts",
            [services.GetRequiredService<ContractsPageViewModel>()],
            PageGroupConstants.ContractGroupId));

        BasicApplicationViewModel.PageGroups.Add(new(
            "Ships",
            [services.GetRequiredService<ShipsPageViewModel>()],
            PageGroupConstants.ShipGroupId));

        BasicApplicationViewModel.PageGroups.Add(new(
            "Waypoints",
            [services.GetRequiredService<WaypointsPageViewModel>()],
            PageGroupConstants.WaypointGroupId));
    }

    public BasicApplicationViewModel BasicApplicationViewModel { get; }
}
