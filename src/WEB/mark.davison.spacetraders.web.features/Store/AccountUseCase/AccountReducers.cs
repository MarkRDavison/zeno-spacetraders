namespace mark.davison.spacetraders.web.features.Store.AccountUseCase;

public static class AccountReducers
{
    [ReducerMethod]
    public static AccountState FetchAccountsAction(AccountState state, FetchAccountsAction action)
    {
        return new AccountState(true, [], []);
    }

    [ReducerMethod]
    public static AccountState FetchAccountsActionResponse(AccountState state, FetchAccountsActionResponse response)
    {
        if (response.SuccessWithValue)
        {
            return new AccountState(false, [.. response.Value], []);
        }

        return new AccountState(false, [], []);
    }

    [ReducerMethod]
    public static AccountState AddAccountActionResponse(AccountState state, AddAccountActionResponse response)
    {
        if (response.SuccessWithValue)
        {
            return new AccountState(
                state.IsLoading,
                [.. state.Accounts.Where(_ => _.Id != response.Value.Id), response.Value],
                state.AccountSummaries);
        }

        return state;
    }

    [ReducerMethod]
    public static AccountState FetchAccountSummaryActionResponse(AccountState state, FetchAccountSummaryActionResponse response)
    {
        if (response.SuccessWithValue)
        {
            return new AccountState(
                state.IsLoading,
                state.Accounts,
                [.. state.AccountSummaries.Where(_ => _.AccountId != response.Value.AccountId), response.Value]);
        }

        return state;
    }
}
