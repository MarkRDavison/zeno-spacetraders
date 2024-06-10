using mark.davison.spacetraders.shared.models.dtos.Commands.ExtractResources;

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

    [EffectMethod]
    public async Task HandlePurchaseShipActionAsync(PurchaseShipAction action, IDispatcher dispatcher)
    {
        var commandRequest = new PurchaseShipCommandRequest
        {
            AccountId = action.AccountId,
            ShipType = action.ShipType,
            WaypointSymbol = action.WaypointSymbol
        };

        var commandResponse = await _repository.Post<PurchaseShipCommandResponse, PurchaseShipCommandRequest>(commandRequest, CancellationToken.None);

        var actionResponse = new PurchaseShipActionResponse
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
    public async Task HandleFetchShipActionAsync(FetchShipAction action, IDispatcher dispatcher)
    {
        var commandRequest = new FetchShipCommandRequest
        {
            AccountId = action.AccountId,
            ShipSymbol = action.ShipSymbol
        };

        var commandResponse = await _repository.Post<FetchShipCommandResponse, FetchShipCommandRequest>(commandRequest, CancellationToken.None);

        var actionResponse = new FetchShipActionResponse
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
    public async Task HandleOrbitShipActionAsync(OrbitShipAction action, IDispatcher dispatcher)
    {
        var commandRequest = new OrbitShipCommandRequest
        {
            AccountId = action.AccountId,
            ShipSymbol = action.ShipSymbol
        };

        var commandResponse = await _repository.Post<OrbitShipCommandResponse, OrbitShipCommandRequest>(commandRequest, CancellationToken.None);

        var actionResponse = new UpdateShipNavResponse
        {
            ActionId = action.ActionId,
            Errors = [.. commandResponse.Errors],
            Warnings = [.. commandResponse.Warnings],
            Value = commandResponse.Value,
            ShipSymbol = commandResponse.ShipSymbol
        };

        // TODO: Framework to dispatch general ***something went wrong***

        dispatcher.Dispatch(actionResponse);
    }

    [EffectMethod]
    public async Task HandleDockShipActionAsync(DockShipAction action, IDispatcher dispatcher)
    {
        var commandRequest = new DockShipCommandRequest
        {
            AccountId = action.AccountId,
            ShipSymbol = action.ShipSymbol
        };

        var commandResponse = await _repository.Post<DockShipCommandResponse, DockShipCommandRequest>(commandRequest, CancellationToken.None);

        var actionResponse = new UpdateShipNavResponse
        {
            ActionId = action.ActionId,
            Errors = [.. commandResponse.Errors],
            Warnings = [.. commandResponse.Warnings],
            Value = commandResponse.Value,
            ShipSymbol = commandResponse.ShipSymbol
        };

        // TODO: Framework to dispatch general ***something went wrong***

        dispatcher.Dispatch(actionResponse);
    }

    [EffectMethod]
    public async Task HandleNavigateShipActionAsync(NavigateShipAction action, IDispatcher dispatcher)
    {
        var commandRequest = new NavigateShipCommandRequest
        {
            AccountId = action.AccountId,
            ShipSymbol = action.ShipSymbol,
            DestinationWaypoint = action.DestinationWaypoint
        };

        var commandResponse = await _repository.Post<NavigateShipCommandResponse, NavigateShipCommandRequest>(commandRequest, CancellationToken.None);

        // TODO: Framework to dispatch general ***something went wrong***

        dispatcher.Dispatch(new UpdateShipNavResponse
        {
            ActionId = action.AccountId,
            Errors = [.. commandResponse.Errors],
            Warnings = [.. commandResponse.Warnings],
            Value = commandResponse.Value.Item1,
            ShipSymbol = action.ShipSymbol
        });
        dispatcher.Dispatch(new UpdateShipFuelResponse
        {
            ActionId = action.AccountId,
            Errors = [.. commandResponse.Errors],
            Warnings = [.. commandResponse.Warnings],
            Value = commandResponse.Value.Item2,
            ShipSymbol = action.ShipSymbol
        });
    }

    [EffectMethod]
    public async Task HandleRefuelShipActionAsync(RefuelShipAction action, IDispatcher dispatcher)
    {
        var commandRequest = new RefuelShipCommandRequest
        {
            AccountId = action.AccountId,
            ShipSymbol = action.ShipSymbol,
            FromCargo = action.FromCargo,
            Units = action.Units
        };

        var commandResponse = await _repository.Post<RefuelShipCommandResponse, RefuelShipCommandRequest>(commandRequest, CancellationToken.None);

        // TODO: Framework to dispatch general ***something went wrong***

        dispatcher.Dispatch(new UpdateShipFuelResponse // TODO: Should this just be an action???
        {
            ActionId = action.AccountId,
            Errors = [.. commandResponse.Errors],
            Warnings = [.. commandResponse.Warnings],
            Value = commandResponse.Value,
            ShipSymbol = action.ShipSymbol
        });
        dispatcher.Dispatch(new UpdateCreditsAction
        {
            ActionId = action.AccountId,
            Credits = commandResponse.Credits
        });
    }

    [EffectMethod]
    public async Task HandleFetchShipCargoActionAsync(FetchShipCargoAction action, IDispatcher dispatcher)
    {
        var queryRequest = new FetchShipCargoQueryRequest
        {
            AccountId = action.AccountId,
            ShipSymbol = action.ShipSymbol
        };

        var queryResponse = await _repository.Get<FetchShipCargoQueryResponse, FetchShipCargoQueryRequest>(queryRequest, CancellationToken.None);

        var actionResponse = new FetchShipCargoActionResponse
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
    public async Task HandleExtractResourceShipActionAsync(ExtractResourceShipAction action, IDispatcher dispatcher)
    {
        var commandRequest = new ExtractResourcesCommandRequest
        {
            AccountId = action.AccountId,
            ShipSymbol = action.ShipSymbol
        };

        var commandResponse = await _repository.Post<ExtractResourcesCommandResponse, ExtractResourcesCommandRequest>(commandRequest, CancellationToken.None);

        // TODO: Framework to dispatch general ***something went wrong***

        dispatcher.Dispatch(new FetchShipCargoActionResponse
        {
            ActionId = action.ActionId,
            Errors = [.. commandResponse.Errors],
            Warnings = [.. commandResponse.Warnings],
            Value = commandResponse.Value
        });
    }
}
