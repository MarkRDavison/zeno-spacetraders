namespace mark.davison.spacetraders.desktop.ui.ViewModels;

public abstract class MainApplicationPageViewModel : BasicApplicationPageViewModel, IDisposable
{
    protected readonly IApplicationNotificationService _applicationNotificationService;
    private readonly IAccountService _accountService;
    private readonly ILogger _logger;

    private readonly List<Action> _disposeActions = [];

    protected MainApplicationPageViewModel(
        IApplicationNotificationService applicationNotificationService,
        ICommonApplicationNotificationService commonApplicationNotificationService,
        IAccountService accountService,
        ILogger logger
    ) : base(
        commonApplicationNotificationService)
    {
        _applicationNotificationService = applicationNotificationService;
        _accountService = accountService;

        _applicationNotificationService.AccountChanged += AccountChanged;
        _logger = logger;
    }

    private void AccountChanged(object? sender, AccountChangeArgs e)
    {
        OnPropertyChanged(nameof(Disabled));
    }

    public override bool Disabled => !_accountService.HasActiveAccount;

    public void Dispose()
    {
        foreach (var a in _disposeActions)
        {
            a();
        }
        _applicationNotificationService.AccountChanged -= AccountChanged;
    }

    protected string AccountIdentifier => _accountService.ActiveAccountIdentifier;

    protected void RegisterStateChange<TState>(IState<TState> state)
        where TState : class, IDesktopState, new()
    {
        state.PropertyChanged += State_PropertyChanged;
        _disposeActions.Add(() => state.PropertyChanged -= State_PropertyChanged);
    }

    private void State_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is StateImplementation si)
        {
            StatePropertyChanged(si.StateValue.GetType().Name);
        }
    }

    protected virtual void StatePropertyChanged(string name) { }
}