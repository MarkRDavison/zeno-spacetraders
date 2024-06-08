namespace mark.davison.spacetraders.web.features.Store.WaypointUseCase;

public sealed class WaypointEffects
{
    private readonly IClientHttpRepository _repository;

    public WaypointEffects(IClientHttpRepository repository)
    {
        _repository = repository;
    }

    [EffectMethod]
    public async Task HandleFetchWaypointsActionAsync(FetchWaypointsAction action, IDispatcher dispatcher)
    {
        var commandRequest = new FetchWaypointsCommandRequest
        {
            AccountId = action.AccountId,
            SystemSymbol = action.SystemSymbol,
            Meta = action.Meta
        };

        var commandResponse = await _repository.Post<FetchWaypointsCommandResponse, FetchWaypointsCommandRequest>(commandRequest, CancellationToken.None);

        var actionResponse = new FetchWaypointsActionResponse
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
