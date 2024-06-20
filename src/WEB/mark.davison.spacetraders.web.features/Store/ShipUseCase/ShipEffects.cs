using mark.davison.spacetraders.shared.models.dtos.Commands.DeliverCargoContract;
using mark.davison.spacetraders.shared.models.dtos.Commands.RefuelShip;

namespace mark.davison.spacetraders.web.features.Store.ShipUseCase;

public sealed class ShipEffects
{
    private readonly IClientHttpRepository _repository;

    public ShipEffects(IClientHttpRepository repository)
    {
        _repository = repository;
    }

    [EffectMethod]
    public async Task HandleFetchShipActionAsync(FetchShipAction action, IDispatcher dispatcher)
    {
        var queryRequest = new FetchShipQueryRequest
        {
            Identifier = action.Identifier,
            ShipSymbol = action.ShipSymbol
        };

        var queryResponse = await _repository.Get<FetchShipQueryResponse, FetchShipQueryRequest>(queryRequest, CancellationToken.None);

        dispatcher.Dispatch(new UpdateShipsActionResponse
        {
            ActionId = action.ActionId,
            Errors = [.. queryResponse.Errors],
            Warnings = [.. queryResponse.Warnings],
            Value = queryResponse.Value is null ? null : [queryResponse.Value]
        });
    }

    [EffectMethod]
    public async Task HandleFetchShipsActionAsync(FetchShipsAction action, IDispatcher dispatcher)
    {
        var queryRequest = new FetchShipsQueryRequest
        {
            Identifier = action.Identifier,
            Page = action.Page,
            Limit = action.Limit
        };

        var queryResponse = await _repository.Get<FetchShipsQueryResponse, FetchShipsQueryRequest>(queryRequest, CancellationToken.None);

        dispatcher.Dispatch(new UpdateShipsActionResponse
        {
            ActionId = action.ActionId,
            Errors = [.. queryResponse.Errors],
            Warnings = [.. queryResponse.Warnings],
            Value = queryResponse.Value
        });
    }

    [EffectMethod]
    public async Task HandleOrbitShipActionAsync(OrbitShipAction action, IDispatcher dispatcher)
    {
        var commandRequest = new ModifyOrbitCommandRequest
        {
            Identifier = action.Identifier,
            ShipSymbol = action.ShipSymbol,
            Dock = false
        };

        var commandResponse = await _repository.Post<ModifyOrbitCommandResponse, ModifyOrbitCommandRequest>(commandRequest, CancellationToken.None);

        dispatcher.Dispatch(new UpdateShipsActionResponse
        {
            ActionId = action.ActionId,
            Errors = [.. commandResponse.Errors],
            Warnings = [.. commandResponse.Warnings],
            Value = commandResponse.Value is null ? null : [commandResponse.Value]
        });
    }

    [EffectMethod]
    public async Task HandleDockShipActionAsync(DockShipAction action, IDispatcher dispatcher)
    {
        var commandRequest = new ModifyOrbitCommandRequest
        {
            Identifier = action.Identifier,
            ShipSymbol = action.ShipSymbol,
            Dock = true
        };

        var commandResponse = await _repository.Post<ModifyOrbitCommandResponse, ModifyOrbitCommandRequest>(commandRequest, CancellationToken.None);

        dispatcher.Dispatch(new UpdateShipsActionResponse
        {
            ActionId = action.ActionId,
            Errors = [.. commandResponse.Errors],
            Warnings = [.. commandResponse.Warnings],
            Value = commandResponse.Value is null ? null : [commandResponse.Value]
        });
    }

    [EffectMethod]
    public async Task HandlePurchaseShipActionAsync(PurchaseShipAction action, IDispatcher dispatcher)
    {
        var commandRequest = new PurchaseShipCommandRequest
        {
            Identifier = action.Identifier,
            WaypointSymbol = action.WaypointSymbol,
            ShipType = action.ShipType
        };

        var commandResponse = await _repository.Post<PurchaseShipCommandResponse, PurchaseShipCommandRequest>(commandRequest, CancellationToken.None);

        if (commandResponse.SuccessWithValue)
        {
            dispatcher.Dispatch(new UpdateShipsActionResponse
            {
                ActionId = action.ActionId,
                Errors = [.. commandResponse.Errors],
                Warnings = [.. commandResponse.Warnings],
                Value = commandResponse.Value?.ShipResponse is null ? null : [commandResponse.Value.ShipResponse]
            });
            dispatcher.Dispatch(new UpdateAgentActionResponse
            {
                ActionId = action.ActionId,
                Errors = [.. commandResponse.Errors],
                Warnings = [.. commandResponse.Warnings],
                Value = commandResponse.Value?.AgentDto
            });
        }
    }

    [EffectMethod]
    public async Task HandleExtractResourcesActionAsync(ExtractResourcesAction action, IDispatcher dispatcher)
    {
        var commandRequest = new ExtractResourcesCommandRequest
        {
            Identifier = action.Identifier,
            ShipSymbol = action.ShipSymbol
        };

        var commandResponse = await _repository.Post<ExtractResourcesCommandResponse, ExtractResourcesCommandRequest>(commandRequest, CancellationToken.None);

        dispatcher.Dispatch(new UpdateShipsActionResponse
        {
            ActionId = action.ActionId,
            Errors = [.. commandResponse.Errors],
            Warnings = [.. commandResponse.Warnings],
            Value = commandResponse.Value is null ? null : [commandResponse.Value]
        });
    }

    [EffectMethod]
    public async Task HandleSellCargoActionAsync(SellCargoAction action, IDispatcher dispatcher)
    {
        var commandRequest = new SellCargoCommandRequest
        {
            Identifier = action.Identifier,
            ShipSymbol = action.ShipSymbol,
            TradeSymbol = action.TradeSymbol,
            Units = action.Units
        };

        var commandResponse = await _repository.Post<SellCargoCommandResponse, SellCargoCommandRequest>(commandRequest, CancellationToken.None);

        if (commandResponse.SuccessWithValue)
        {
            dispatcher.Dispatch(new UpdateShipsActionResponse
            {
                ActionId = action.ActionId,
                Errors = [.. commandResponse.Errors],
                Warnings = [.. commandResponse.Warnings],
                Value = commandResponse.Value?.ShipResponse is null ? null : [commandResponse.Value.ShipResponse]
            });
            dispatcher.Dispatch(new UpdateAgentActionResponse
            {
                ActionId = action.ActionId,
                Errors = [.. commandResponse.Errors],
                Warnings = [.. commandResponse.Warnings],
                Value = commandResponse.Value?.AgentDto
            });
        }
    }

    [EffectMethod]
    public async Task HandleJettisonCargoActionAsync(JettisonCargoAction action, IDispatcher dispatcher)
    {
        var commandRequest = new JettisonCargoCommandRequest
        {
            Identifier = action.Identifier,
            ShipSymbol = action.ShipSymbol,
            TradeSymbol = action.TradeSymbol,
            Units = action.Units
        };

        var commandResponse = await _repository.Post<JettisonCargoCommandResponse, JettisonCargoCommandRequest>(commandRequest, CancellationToken.None);

        if (commandResponse.SuccessWithValue)
        {
            dispatcher.Dispatch(new UpdateShipsActionResponse
            {
                ActionId = action.ActionId,
                Errors = [.. commandResponse.Errors],
                Warnings = [.. commandResponse.Warnings],
                Value = commandResponse.Value is null ? null : [commandResponse.Value]
            });
        }
    }

    [EffectMethod]
    public async Task HandleNavigateShipActionAsync(NavigateShipAction action, IDispatcher dispatcher)
    {
        var commandRequest = new NavigateShipCommandRequest
        {
            Identifier = action.Identifier,
            ShipSymbol = action.ShipSymbol,
            DestinationWaypoint = action.DestinationWaypoint
        };

        var commandResponse = await _repository.Post<NavigateShipCommandResponse, NavigateShipCommandRequest>(commandRequest, CancellationToken.None);

        if (commandResponse.SuccessWithValue)
        {
            dispatcher.Dispatch(new UpdateShipsActionResponse
            {
                ActionId = action.ActionId,
                Errors = [.. commandResponse.Errors],
                Warnings = [.. commandResponse.Warnings],
                Value = commandResponse.Value is null ? null : [commandResponse.Value]
            });
        }
    }

    [EffectMethod]
    public async Task HandleDeliverContractCargoActionAsync(DeliverContractCargoAction action, IDispatcher dispatcher)
    {
        var commandRequest = new DeliverContractCargoCommandRequest
        {
            Identifier = action.Identifier,
            ContractId = action.ContractId,
            ShipSymbol = action.ShipSymbol,
            TradeSymbol = action.TradeSymbol,
            Units = action.Units
        };

        var commandResponse = await _repository.Post<DeliverContractCargoCommandResponse, DeliverContractCargoCommandRequest>(commandRequest, CancellationToken.None);

        if (commandResponse.SuccessWithValue)
        {
            dispatcher.Dispatch(new UpdateShipsActionResponse
            {
                ActionId = action.ActionId,
                Errors = [.. commandResponse.Errors],
                Warnings = [.. commandResponse.Warnings],
                Value = commandResponse.Value?.ShipResponse is null ? null : [commandResponse.Value.ShipResponse]
            });
            dispatcher.Dispatch(new UpdateAgentActionResponse
            {
                ActionId = action.ActionId,
                Errors = [.. commandResponse.Errors],
                Warnings = [.. commandResponse.Warnings],
                Value = commandResponse.Value?.AgentDto
            });
        }
    }

    [EffectMethod]
    public async Task HandleRefuelShipActionAsync(RefuelShipAction action, IDispatcher dispatcher)
    {
        var commandRequest = new RefuelShipCommandRequest
        {
            Identifier = action.Identifier,
            ShipSymbol = action.ShipSymbol,
            Units = action.Units,
            FromCargo = action.FromCargo
        };

        var commandResponse = await _repository.Post<RefuelShipCommandResponse, RefuelShipCommandRequest>(commandRequest, CancellationToken.None);

        if (commandResponse.SuccessWithValue)
        {
            dispatcher.Dispatch(new UpdateShipsActionResponse
            {
                ActionId = action.ActionId,
                Errors = [.. commandResponse.Errors],
                Warnings = [.. commandResponse.Warnings],
                Value = commandResponse.Value?.ShipResponse is null ? null : [commandResponse.Value.ShipResponse]
            });
            dispatcher.Dispatch(new UpdateAgentActionResponse
            {
                ActionId = action.ActionId,
                Errors = [.. commandResponse.Errors],
                Warnings = [.. commandResponse.Warnings],
                Value = commandResponse.Value?.AgentDto
            });
        }
    }
}
