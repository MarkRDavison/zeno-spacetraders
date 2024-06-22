namespace mark.davison.spacetraders.desktop.ui.ViewModels;

public partial class ContractsPageViewModel : MainApplicationPageViewModel
{
    public ContractsPageViewModel(
        IApplicationNotificationService applicationNotificationService,
        IAccountService accountService
    ) : base(
        applicationNotificationService,
        accountService)
    {
    }

    public override string Name => "Contracts";
}
