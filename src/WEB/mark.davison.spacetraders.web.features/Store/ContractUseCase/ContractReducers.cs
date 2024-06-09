namespace mark.davison.spacetraders.web.features.Store.ContractUseCase;

public static class ContractReducers
{
    [ReducerMethod]
    public static ContractState FetchContractsAction(ContractState state, FetchContractsAction action)
    {
        return new ContractState(true, []);
    }

    [ReducerMethod]
    public static ContractState FetchContractsActionResponse(ContractState state, FetchContractsActionResponse response)
    {
        if (response.SuccessWithValue)
        {
            var newIds = response.Value.Select(_ => _.Id).ToHashSet();

            return new ContractState(false,
                [
                    .. state.Contracts.Where(_ => !newIds.Contains(_.Id)),
                    .. response.Value
                ]);
        }

        return new ContractState(false, state.Contracts);
    }

    [ReducerMethod]
    public static ContractState AcceptContractActionResponse(ContractState state, AcceptContractActionResponse response)
    {
        if (response.SuccessWithValue)
        {
            return new ContractState(false,
                [
                    .. state.Contracts.Where(_ => _.Id != response.Value.Id),
                    response.Value
                ]);
        }

        return new ContractState(false, state.Contracts);
    }
}
