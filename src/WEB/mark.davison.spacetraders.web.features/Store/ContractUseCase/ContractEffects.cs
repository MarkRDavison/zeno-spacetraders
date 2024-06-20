namespace mark.davison.spacetraders.web.features.Store.ContractUseCase;

public sealed class ContractEffects
{
    private readonly IClientHttpRepository _repository;

    public ContractEffects(IClientHttpRepository repository)
    {
        _repository = repository;
    }

    [EffectMethod]
    public async Task HandleFetchContractsActionAsync(OldFetchContractsAction action, IDispatcher dispatcher)
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

    [EffectMethod]
    public async Task HandleAcceptContractActionAsync(AcceptContractAction action, IDispatcher dispatcher)
    {
        var commandRequest = new AcceptContractCommandRequest
        {
            AccountId = action.AccountId,
            ContractId = action.ContractId
        };

        var commandResponse = await _repository.Post<AcceptContractCommandResponse, AcceptContractCommandRequest>(commandRequest, CancellationToken.None);

        var actionResponse = new AcceptContractActionResponse
        {
            ActionId = action.ActionId,
            Errors = [.. commandResponse.Errors],
            Warnings = [.. commandResponse.Warnings],
            Value = commandResponse.Value
        };

        // TODO: Framework to dispatch general ***something went wrong***

        dispatcher.Dispatch(actionResponse);
        dispatcher.Dispatch(new UpdateCreditsAction
        {
            ActionId = action.ActionId,
            AccountId = action.AccountId,
            Credits = commandResponse.Credits
        });
    }

    [EffectMethod]
    public async Task HandleFetchContractActionAsync(FetchContractAction action, IDispatcher dispatcher)
    {
        var queryRequest = new FetchContractQueryRequest
        {
            Identifier = action.Identifier,
            ContractId = action.ContractId
        };

        var queryResponse = await _repository.Get<FetchContractQueryResponse, FetchContractQueryRequest>(queryRequest, CancellationToken.None);

        var actionResponse = new UpdateContractsActionResponse
        {
            ActionId = action.ActionId,
            Errors = [.. queryResponse.Errors],
            Warnings = [.. queryResponse.Warnings],
            Value = queryResponse.Value == null ? null : [queryResponse.Value]
        };

        // TODO: Framework to dispatch general ***something went wrong***

        dispatcher.Dispatch(actionResponse);
    }


    [EffectMethod]
    public async Task HandleFetchContractsActionAsync(FetchContractsAction action, IDispatcher dispatcher)
    {
        var queryRequest = new FetchContractsQueryRequest
        {
            Identifier = action.Identifier
        };

        var queryResponse = await _repository.Get<FetchContractsQueryResponse, FetchContractsQueryRequest>(queryRequest, CancellationToken.None);

        var actionResponse = new UpdateContractsActionResponse
        {
            ActionId = action.ActionId,
            Errors = [.. queryResponse.Errors],
            Warnings = [.. queryResponse.Warnings],
            Value = queryResponse.Value
        };

        // TODO: Framework to dispatch general ***something went wrong***

        dispatcher.Dispatch(actionResponse);
    }
}
