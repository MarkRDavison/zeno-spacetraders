namespace mark.davison.spacetraders.avalonia.ui.Services;

public sealed class AccountContext : IAccountContext
{
    private PersistedAccount? _activeAccount;
    private readonly IAuthenticationContext _authenticationContext;
    private readonly IApplicationNotificationService _applicationNotificationService;

    public AccountContext(
        IAuthenticationContext authenticationContext,
        IApplicationNotificationService applicationNotificationService)
    {
        _authenticationContext = authenticationContext;
        _applicationNotificationService = applicationNotificationService;
    }

    public PersistedAccount GetActiveAccount()
    {
        return _activeAccount ?? throw new InvalidOperationException("Account not set");
    }

    public void SetActiveAccount(PersistedAccount account)
    {
        _activeAccount = account;
        _authenticationContext.Token = account.Token;
        _applicationNotificationService.OnUserChanged(account);
    }
}
