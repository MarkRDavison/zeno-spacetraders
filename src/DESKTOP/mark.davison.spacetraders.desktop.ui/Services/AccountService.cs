﻿namespace mark.davison.spacetraders.desktop.ui.Services;

internal sealed class AccountService : IAccountService
{
    private readonly IApplicationNotificationService _applicationNotificationService;

    public AccountService(IApplicationNotificationService applicationNotificationService)
    {
        _applicationNotificationService = applicationNotificationService;
    }

    private AccountModel? _activeAccount;
    private AgentModel? _agentModel;

    public bool HasActiveAccount => _activeAccount is not null;

    public AccountModel GetActiveAccount()
    {
        return _activeAccount ?? throw new InvalidOperationException("No active account");
    }

    public void SetActiveAccount(AccountModel account)
    {
        if (_activeAccount?.Id != account.Id)
        {
            _activeAccount = account;
            _applicationNotificationService.ChangeAccount();
        }
    }
    public AgentModel? GetActiveAccountAgent() => _agentModel;
    public void SetActiveAccountAgent(AgentModel agent)
    {
        _agentModel = agent;
        _applicationNotificationService.ChangeAgent();
    }
}