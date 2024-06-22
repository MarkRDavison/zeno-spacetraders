namespace mark.davison.spacetraders.desktop.ui.ViewModels;

public abstract class MainApplicationPageViewModel : BasicApplicationPageViewModel, IDisposable
{
    private readonly IApplicationNotificationService _applicationNotificationService;
    private readonly IAccountService _accountService;

    protected MainApplicationPageViewModel(
        IApplicationNotificationService applicationNotificationService,
        IAccountService accountService)
    {
        _applicationNotificationService = applicationNotificationService;
        _accountService = accountService;

        _applicationNotificationService.AccountChanged += AccountChanged;
    }

    private void AccountChanged(object? sender, AccountChangeArgs e)
    {
        OnPropertyChanged(nameof(Disabled));
    }

    public override bool Disabled => !_accountService.HasActiveAccount;

    public void Dispose()
    {
        _applicationNotificationService.AccountChanged -= AccountChanged;
    }
}