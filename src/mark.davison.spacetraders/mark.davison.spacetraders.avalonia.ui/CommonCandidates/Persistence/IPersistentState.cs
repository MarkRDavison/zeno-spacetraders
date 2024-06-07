namespace mark.davison.spacetraders.avalonia.ui.CommonCandidates.Persistence;

public interface IPersistentState
{
    List<PersistedAccount> GetAccounts();
    Task<List<PersistedAccount>> LoadAccountsAsync(CancellationToken cancellationToken);
    Task AddAccountAsync(string token, CancellationToken cancellationToken);
    Task SaveAccountsAsync(CancellationToken cancellationToken);
    void Reset(CancellationToken cancellationToken);
    Task AwaitAccountLoadingAsync();
}
