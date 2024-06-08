namespace mark.davison.spacetraders.web.features.Store.AccountUseCase;

public sealed class AccountEffects
{
    private readonly IClientHttpRepository _repository;

    public AccountEffects(IClientHttpRepository repository)
    {
        _repository = repository;
    }

    [EffectMethod]
    public async Task HandleFetchAccountsActionAsync(FetchAccountsAction action, IDispatcher dispatcher)
    {
        var queryRequest = new FetchAccountsQueryRequest();

        var queryResponse = await _repository.Get<FetchAccountsQueryResponse, FetchAccountsQueryRequest>(queryRequest, CancellationToken.None);

        var actionResponse = new FetchAccountsActionResponse
        {
            ActionId = action.ActionId,
            Errors = [.. queryResponse.Errors],
            Warnings = [.. queryResponse.Warnings],
            Value = queryResponse.Value
        };

        // TODO: Framework to dispatch general ***something went wrong***

        dispatcher.Dispatch(actionResponse);
    }

    [EffectMethod]
    public async Task HandleAddAccountActionAsync(AddAccountAction action, IDispatcher dispatcher)
    {
        var commandRequest = new AddAccountCommandRequest
        {
            AddExisting = action.AddExisting,
            Token = action.Token,
            Email = action.Email,
            Identifier = action.Identifier,
            FactionSymbol = action.FactionSymbol
        };

        var commandResponse = await _repository.Post<AddAccountCommandResponse, AddAccountCommandRequest>(commandRequest, CancellationToken.None);

        var actionResponse = new AddAccountActionResponse
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
    public async Task HandleFetchAccountSummaryActionAsync(FetchAccountSummaryAction action, IDispatcher dispatcher)
    {
        var queryRequest = new FetchAccountSummaryQueryRequest
        {
            AccountId = action.AccountId
        };

        var queryResponse = await _repository.Get<FetchAccountSummaryQueryResponse, FetchAccountSummaryQueryRequest>(queryRequest, CancellationToken.None);

        var actionResponse = new FetchAccountSummaryActionResponse
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
