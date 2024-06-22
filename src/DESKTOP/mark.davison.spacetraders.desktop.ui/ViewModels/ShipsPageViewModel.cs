namespace mark.davison.spacetraders.desktop.ui.ViewModels;

public partial class ShipsPageViewModel : MainApplicationPageViewModel
{
    public ShipsPageViewModel(
        IApplicationNotificationService applicationNotificationService,
        IAccountService accountService
    ) : base(
        applicationNotificationService,
        accountService)
    {
    }

    public override string Name => "Ships";
}
