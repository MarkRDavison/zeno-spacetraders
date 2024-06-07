namespace mark.davison.spacetraders.avalonia.ui.ViewModels;

public partial class LandingPageViewModel : PageViewModel
{
    private readonly IPersistentState _persistentState;
    private readonly IAccountContext _accountContext;

    public LandingPageViewModel() : this(
        DependencyInjectionExtensions.DesignTimeServiceProvider.GetRequiredService<IPersistentState>(),
        DependencyInjectionExtensions.DesignTimeServiceProvider.GetRequiredService<IAccountContext>())
    {

    }

    public LandingPageViewModel(
        IPersistentState persistentState,
        IAccountContext accountContext)
    {
        RegisterViewModel = new();
        SignInViewModel = new();
        _persistentState = persistentState;
        _accountContext = accountContext;

        Accounts = new();

        _ = Task.Run(async () =>
        {
            await _persistentState.AwaitAccountLoadingAsync();

            Accounts.Clear();

            foreach (var account in _persistentState.GetAccounts())
            {
                Accounts.Add(account);
            }
        });
    }

    [RelayCommand]
    private void SignIn()
    {
        ShowSignIn = true;
        ShowRegister = false;
    }

    [RelayCommand]
    private void Register()
    {
        ShowRegister = true;
        ShowSignIn = false;
    }

    [RelayCommand]
    private void Cancel()
    {
        ShowRegister = false;
        ShowSignIn = false;
    }


    [RelayCommand]
    private async Task Submit(CancellationToken cancellationToken)
    {
        if (ShowRegister)
        {
            await RegisterViewModel.RegisterCommand.ExecuteAsync(null);
        }
        else if (ShowSignIn)
        {

        }
    }

    partial void OnActiveAccountChanged(PersistedAccount? oldValue, PersistedAccount? newValue)
    {
        if (newValue != null)
        {
            _accountContext.SetActiveAccount(newValue);
        }
    }

    public ObservableCollection<PersistedAccount> Accounts { get; }

    [ObservableProperty]
    private PersistedAccount? _activeAccount;

    [ObservableProperty]
    private RegisterViewModel _registerViewModel;

    [ObservableProperty]
    private SignInViewModel _signInViewModel;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShowingChild))]
    [NotifyPropertyChangedFor(nameof(ShowSignIn))]
    private bool _showRegister;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShowingChild))]
    [NotifyPropertyChangedFor(nameof(ShowRegister))]
    private bool _showSignIn;

    public bool ShowingChild => ShowRegister || ShowSignIn;

}
