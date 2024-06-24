namespace mark.davison.spacetraders.desktop.ui.Store.ContractUseCase;

public static class ContractReducers
{
    [DesktopReducer]
    public static ContractState HandleResetStateAction(ContractState state, ResetStateAction action)
    {
        return new();
    }

    [DesktopReducer]
    public static ContractState HandleFetchContractsAction(ContractState state, FetchContractsAction response)
    {
        return new ContractState(true, []);
    }

    [DesktopReducer]
    public static ContractState HandleUpdateContractsActionResponse(ContractState state, UpdateContractsActionResponse response)
    {
        if (response.SuccessWithValue)
        {
            return new ContractState(false, response.Value);
        }

        return new ContractState(false, []);
    }
}
