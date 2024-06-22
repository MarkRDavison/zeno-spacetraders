namespace mark.davison.spacetraders.desktop.ui.ViewModels;

public partial class AgentSummaryViewModel : ObservableObject, IDisposable
{
    private readonly IAccountService _accountService;
    private readonly IApplicationNotificationService _applicationNotificationService;

    public AgentSummaryViewModel(
        IAccountService accountService,
        IApplicationNotificationService applicationNotificationService)
    {
        _accountService = accountService;
        _applicationNotificationService = applicationNotificationService;

        _applicationNotificationService.AccountChanged += AccountChanged;
        _applicationNotificationService.AgentChanged += AgentChanged;
    }

    private void AgentChanged(object? sender, AgentChangeArgs e) => UpdateUi();

    private void AccountChanged(object? sender, AccountChangeArgs e) => UpdateUi();

    private void UpdateUi()
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            OnPropertyChanged(nameof(Identifier));
            OnPropertyChanged(nameof(Credits));
            OnPropertyChanged(nameof(Headquarters));
        });
    }

    public string? Identifier => _accountService.HasActiveAccount ? _accountService.GetActiveAccount().Identifier : null;

    public long? Credits => _accountService.GetActiveAccountAgent()?.Credits;
    public string? Headquarters => _accountService.GetActiveAccountAgent()?.Headquarters;
    public int? ShipCount => _accountService.GetActiveAccountAgent()?.ShipCount;

    public void Dispose()
    {
        _applicationNotificationService.AgentChanged -= AgentChanged;
        _applicationNotificationService.AccountChanged -= AccountChanged;
    }
}
