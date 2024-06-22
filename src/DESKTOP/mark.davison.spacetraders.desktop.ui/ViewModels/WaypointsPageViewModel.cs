namespace mark.davison.spacetraders.desktop.ui.ViewModels;

public partial class WaypointsPageViewModel : MainApplicationPageViewModel
{
    public WaypointsPageViewModel(
        IApplicationNotificationService applicationNotificationService,
        IAccountService accountService
    ) : base(
        applicationNotificationService,
        accountService)
    {
    }

    public override string Name => "Waypoints";
}
