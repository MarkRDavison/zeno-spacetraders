namespace mark.davison.spacetraders.web.features.Store.ContractUseCase;

public static class ContractReducers
{
    [ReducerMethod]
    public static OldContractState FetchContractsAction(OldContractState state, OldFetchContractsAction action)
    {
        return new OldContractState(true, []);
    }

    [ReducerMethod]
    public static OldContractState FetchContractsActionResponse(OldContractState state, FetchContractsActionResponse response)
    {
        if (response.SuccessWithValue)
        {
            var newIds = response.Value.Select(_ => _.Id).ToHashSet();

            return new OldContractState(false,
                [
                    .. state.Contracts.Where(_ => !newIds.Contains(_.Id)),
                    .. response.Value
                ]);
        }

        return new OldContractState(false, state.Contracts);
    }

    [ReducerMethod]
    public static OldContractState AcceptContractActionResponse(OldContractState state, AcceptContractActionResponse response)
    {
        if (response.SuccessWithValue)
        {
            return new OldContractState(false,
                [
                    .. state.Contracts.Where(_ => _.Id != response.Value.Id),
                    response.Value
                ]);
        }

        return new OldContractState(false, state.Contracts);
    }

    [ReducerMethod]
    public static ContractState UpdateContractsActionResponse(ContractState state, UpdateContractsActionResponse response)
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
}
