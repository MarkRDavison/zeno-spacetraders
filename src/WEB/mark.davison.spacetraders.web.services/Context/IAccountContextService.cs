namespace mark.davison.spacetraders.web.services.Context;

public interface IAccountContextService
{
    Task ActivateAccountAsync(AccountDto account, CancellationToken cancellationToken);
    void DeactivateAccount();
    AccountDto? GetActiveAccount();
    event EventHandler<AccountDto?> ActiveAccountChanged;
}
