namespace mark.davison.spacetraders.desktop.ui.Services;

public interface IAccountService
{
    bool HasActiveAccount { get; }
    AccountModel GetActiveAccount();
    string ActiveAccountIdentifier { get; }
    void SetActiveAccount(AccountModel account);
    void UnselectActiveAccount();
    AgentDto? GetActiveAccountAgent();
    void SetActiveAccountAgent(AgentDto agent);
}
