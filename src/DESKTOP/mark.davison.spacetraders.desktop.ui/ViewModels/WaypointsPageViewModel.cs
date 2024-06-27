namespace mark.davison.spacetraders.desktop.ui.ViewModels;

public partial class WaypointsPageViewModel : MainApplicationPageViewModel
{
    public WaypointsPageViewModel(
        IApplicationNotificationService applicationNotificationService,
        ICommonApplicationNotificationService commonApplicationNotificationService,
        IAccountService accountService,
        ILogger<WaypointsPageViewModel> logger
    ) : base(
        applicationNotificationService,
        commonApplicationNotificationService,
        accountService,
        logger)
    {
    }

    public override string Name => "Waypoints";
    public override string Id => PageConstants.WaypointsPageId;
}
