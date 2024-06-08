namespace mark.davison.spacetraders.web.features.Store.ShipUseCase;

public sealed class ShipEffects
{
    private readonly IClientHttpRepository _repository;

    public ShipEffects(IClientHttpRepository repository)
    {
        _repository = repository;
    }

    [EffectMethod]
    public async Task HandleFetchShipsActionAsync(FetchShipsAction action, IDispatcher dispatcher)
    {
        var commandRequest = new FetchShipsCommandRequest
        {
            AccountId = action.AccountId,
            Meta = action.Meta
        };

        var commandResponse = await _repository.Post<FetchShipsCommandResponse, FetchShipsCommandRequest>(commandRequest, CancellationToken.None);

        var actionResponse = new FetchShipsActionResponse
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
