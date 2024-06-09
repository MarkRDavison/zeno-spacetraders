using mark.davison.spacetraders.shared.models.dtos.Commands.FetchWaypoint;

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
            Trait = action.Trait,
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

    [EffectMethod]
    public async Task HandleFetchWaypointActionAsync(FetchWaypointAction action, IDispatcher dispatcher)
    {
        var commandRequest = new FetchWaypointCommandRequest
        {
            AccountId = action.AccountId,
            WaypointSymbol = action.WaypointSymbol
        };

        var commandResponse = await _repository.Post<FetchWaypointCommandResponse, FetchWaypointCommandRequest>(commandRequest, CancellationToken.None);

        var actionResponse = new FetchWaypointActionResponse
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
    public async Task HandleFetchShipyardActionAsync(FetchShipyardAction action, IDispatcher dispatcher)
    {
        var commandRequest = new FetchShipyardCommandRequest
        {
            AccountId = action.AccountId,
            SystemSymbol = action.SystemSymbol,
            WaypointSymbol = action.WaypointSymbol
        };

        var commandResponse = await _repository.Post<FetchShipyardCommandResponse, FetchShipyardCommandRequest>(commandRequest, CancellationToken.None);

        var actionResponse = new FetchShipyardActionResponse
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
