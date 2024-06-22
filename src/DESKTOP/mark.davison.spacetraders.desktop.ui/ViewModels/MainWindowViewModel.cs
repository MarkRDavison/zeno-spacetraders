namespace mark.davison.spacetraders.desktop.ui.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly IApplicationNotificationService _applicationNotificationService;
    public MainWindowViewModel(IServiceProvider services)
    {
        _applicationNotificationService = services.GetRequiredService<IApplicationNotificationService>();

        BasicApplicationViewModel = new BasicApplicationViewModel(
            "Spacetraders",
            services)
        {
            AppBarChildContentViewModel = services.GetRequiredService<AgentSummaryViewModel>()
        };

        BasicApplicationViewModel.Pages.Add(services.GetRequiredService<AccountsPageViewModel>());
        BasicApplicationViewModel.Pages.Add(services.GetRequiredService<ContractsPageViewModel>());
        BasicApplicationViewModel.Pages.Add(services.GetRequiredService<ShipsPageViewModel>());
        BasicApplicationViewModel.Pages.Add(services.GetRequiredService<WaypointsPageViewModel>());

        _applicationNotificationService.PageChanged += OnPageChanged;
    }

    private void OnPageChanged(object? sender, PageEventArgs e)
    {
        Dispatcher.UIThread.Invoke(() => BasicApplicationViewModel.SelectedPageIndex = e.Page switch
        {
            Page.Accounts => 0,
            Page.Contracts => 1,
            Page.Ships => 2,
            Page.Waypoints => 3,
            _ => BasicApplicationViewModel.SelectedPageIndex
        });
    }

    public BasicApplicationViewModel BasicApplicationViewModel { get; }
}
