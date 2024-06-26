namespace mark.davison.spacetraders.desktop.ui.Services;

internal sealed class AccountService : IAccountService
{
    private readonly IApplicationNotificationService _applicationNotificationService;

    public AccountService(IApplicationNotificationService applicationNotificationService)
    {
        _applicationNotificationService = applicationNotificationService;
    }

    private AccountModel? _activeAccount;
    private AgentDto? _agentModel;
    private ServerStatusDto? _serverStatus;

    public bool HasActiveAccount => _activeAccount is not null;

    public AccountModel GetActiveAccount()
    {
        return _activeAccount ?? throw new InvalidOperationException("No active account");
    }

    public string ActiveAccountIdentifier => GetActiveAccount().Identifier;

    public void SetActiveAccount(AccountModel account)
    {
        if (_activeAccount?.Id != account.Id)
        {
            _agentModel = null;
            _activeAccount = account;
            _applicationNotificationService.ChangeAccount();
        }
    }
    public void UnselectActiveAccount()
    {
        _activeAccount = null;
        _applicationNotificationService.ChangeAccount();
    }
    public AgentDto? GetActiveAccountAgent() => _agentModel;
    public void SetActiveAccountAgent(AgentDto agent)
    {
        _agentModel = agent;
        _applicationNotificationService.ChangeAgent();
    }
    public ServerStatusDto? GetServerStatus() => _serverStatus;
    public void SetServerStatus(ServerStatusDto? serverStatus)
    {
        _serverStatus = serverStatus;
    }
}
