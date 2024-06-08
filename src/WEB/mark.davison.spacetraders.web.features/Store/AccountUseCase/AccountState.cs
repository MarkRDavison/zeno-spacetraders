namespace mark.davison.spacetraders.web.features.Store.AccountUseCase;

[FeatureState]
public sealed class AccountState
{
    public bool IsLoading { get; }
    public ReadOnlyCollection<AccountDto> Accounts { get; }
    public ReadOnlyCollection<AccountSummaryDto> AccountSummaries { get; }

    public AccountState() : this(false, [], [])
    {

    }

    public AccountState(
        bool isLoading,
        IEnumerable<AccountDto> accounts,
        IEnumerable<AccountSummaryDto> accountSummaries)
    {
        IsLoading = isLoading;
        Accounts = new ReadOnlyCollection<AccountDto>(accounts.ToList());
        AccountSummaries = new ReadOnlyCollection<AccountSummaryDto>(accountSummaries.ToList());
    }
}