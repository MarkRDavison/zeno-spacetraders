namespace mark.davison.spacetraders.avalonia.ui.Services;

public interface IAccountContext
{
    PersistedAccount GetActiveAccount();
    void SetActiveAccount(PersistedAccount account);
}
