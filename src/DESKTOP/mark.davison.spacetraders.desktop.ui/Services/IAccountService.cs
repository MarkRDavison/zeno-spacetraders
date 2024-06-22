namespace mark.davison.spacetraders.desktop.ui.Services;

public interface IAccountService
{
    bool HasActiveAccount { get; }
    AccountModel GetActiveAccount();
    void SetActiveAccount(AccountModel account);
    AgentModel? GetActiveAccountAgent();
    void SetActiveAccountAgent(AgentModel agent);
}
