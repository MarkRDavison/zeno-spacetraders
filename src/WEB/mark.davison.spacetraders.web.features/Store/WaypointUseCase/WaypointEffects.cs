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
        var queryRequest = new FetchWaypointsQueryRequest
        {
            Identifier = action.Identifier,
            Page = action.Page,
            Limit = action.Limit,
            SystemSymbol = action.SystemSymbol,
            WaypointTrait = action.WaypointTrait,
            WaypointType = action.WaypointType
        };

        var commandResponse = await _repository.Get<FetchWaypointsQueryResponse, FetchWaypointsQueryRequest>(queryRequest, CancellationToken.None);

        var actionResponse = new UpdateWaypointsActionResponse
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
        var queryRequest = new FetchWaypointQueryRequest
        {
            Identifier = action.Identifier,
            SystemSymbol = action.SystemSymbol,
            WaypointSymbol = action.WaypointSymbol
        };

        var queryResponse = await _repository.Get<FetchWaypointQueryResponse, FetchWaypointQueryRequest>(queryRequest, CancellationToken.None);

        var actionResponse = new UpdateWaypointsActionResponse
        {
            ActionId = action.ActionId,
            Errors = [.. queryResponse.Errors],
            Warnings = [.. queryResponse.Warnings],
            Value = queryResponse.Value is null ? null : [queryResponse.Value]
        };

        // TODO: Framework to dispatch general ***something went wrong***

        dispatcher.Dispatch(actionResponse);
    }

    [EffectMethod]
    public async Task HandleFetchShipyardActionAsync(FetchShipyardAction action, IDispatcher dispatcher)
    {
        var queryRequest = new FetchShipyardQueryRequest
        {
            Identifier = action.Identifier,
            SystemSymbol = action.SystemSymbol,
            WaypointSymbol = action.WaypointSymbol
        };

        var queryResponse = await _repository.Get<FetchShipyardQueryResponse, FetchShipyardQueryRequest>(queryRequest, CancellationToken.None);

        var actionResponse = new UpdateWaypointsActionResponse
        {
            ActionId = action.ActionId,
            Errors = [.. queryResponse.Errors],
            Warnings = [.. queryResponse.Warnings],
            Value = queryResponse.Value is null ? null : [queryResponse.Value]
        };

        // TODO: Framework to dispatch general ***something went wrong***

        dispatcher.Dispatch(actionResponse);
    }

    [EffectMethod]
    public async Task HandleFetchMarketplaceActionAsync(FetchMarketplaceAction action, IDispatcher dispatcher)
    {
        var queryRequest = new FetchMarketplaceQueryRequest
        {
            Identifier = action.Identifier,
            SystemSymbol = action.SystemSymbol,
            WaypointSymbol = action.WaypointSymbol
        };

        var queryResponse = await _repository.Get<FetchMarketplaceQueryResponse, FetchMarketplaceQueryRequest>(queryRequest, CancellationToken.None);

        var actionResponse = new UpdateWaypointsActionResponse
        {
            ActionId = action.ActionId,
            Errors = [.. queryResponse.Errors],
            Warnings = [.. queryResponse.Warnings],
            Value = queryResponse.Value is null ? null : [queryResponse.Value]
        };

        // TODO: Framework to dispatch general ***something went wrong***

        dispatcher.Dispatch(actionResponse);
    }
}
