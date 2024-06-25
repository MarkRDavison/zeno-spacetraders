using mark.davison.spacetraders.desktop.ui.Constants;

namespace mark.davison.spacetraders.desktop.ui.ViewModels;

public partial class WaypointsPageViewModel : MainApplicationPageViewModel
{
    public WaypointsPageViewModel(
        IApplicationNotificationService applicationNotificationService,
        IAccountService accountService,
        ILogger<WaypointsPageViewModel> logger
    ) : base(
        applicationNotificationService,
        accountService,
        logger)
    {
    }

    public override string Name => "Waypoints";
    public override string Id => PageConstants.WaypointsPageId;
}
