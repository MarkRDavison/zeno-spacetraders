namespace mark.davison.spacetraders.web.services.Context;

public sealed class AccountContextService : IAccountContextService
{
    private AccountDto? _activeAccount = null;
    private readonly IStoreHelper _storeHelper;

    public AccountContextService(IStoreHelper storeHelper)
    {
        _storeHelper = storeHelper;
    }

    public async Task ActivateAccountAsync(AccountDto account, CancellationToken cancellationToken)
    {
        await _storeHelper.DispatchAndWaitForResponse<FetchAccountSummaryAction, FetchAccountSummaryActionResponse>(new()
        {
            AccountId = account.Id
        });
        _activeAccount = account;
        ActiveAccountChanged?.Invoke(this, account);
    }

    public void DeactivateAccount()
    {
        _activeAccount = null;
        ActiveAccountChanged?.Invoke(this, null);
    }

    public AccountDto? GetActiveAccount() => _activeAccount;

    public event EventHandler<AccountDto?> ActiveAccountChanged = default!;
}
