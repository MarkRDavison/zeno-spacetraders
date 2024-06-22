namespace mark.davison.spacetraders.desktop.ui.ViewModels;

public partial class ShipsPageViewModel : MainApplicationPageViewModel
{
    public ShipsPageViewModel(
        IApplicationNotificationService applicationNotificationService,
        IAccountService accountService,
        ILogger<ShipsPageViewModel> logger
    ) : base(
        applicationNotificationService,
        accountService,
        logger)
    {
    }

    public override string Name => "Ships";
}
