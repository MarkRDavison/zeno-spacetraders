namespace mark.davison.spacetraders.web.features.Store.ContractUseCase;

public sealed class ContractEffects
{
    private readonly IClientHttpRepository _repository;

    public ContractEffects(IClientHttpRepository repository)
    {
        _repository = repository;
    }

    [EffectMethod]
    public async Task HandleFetchContractsActionAsync(FetchContractsAction action, IDispatcher dispatcher)
    {
        var commandRequest = new FetchContractsCommandRequest
        {
            AccountId = action.AccountId,
            Meta = action.Meta
        };

        var commandResponse = await _repository.Post<FetchContractsCommandResponse, FetchContractsCommandRequest>(commandRequest, CancellationToken.None);

        var actionResponse = new FetchContractsActionResponse
        {
            ActionId = action.ActionId,
            Errors = [.. commandResponse.Errors],
            Warnings = [.. commandResponse.Warnings],
            Value = commandResponse.Value
        };

        // TODO: Framework to dispatch general ***something went wrong***

        dispatcher.Dispatch(actionResponse);
    }
}
