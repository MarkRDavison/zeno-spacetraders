using mark.davison.spacetraders.shared.models.dtos.Commands.FetchShip;
using mark.davison.spacetraders.shared.models.dtos.Commands.PurchaseShip;
using mark.davison.spacetraders.web.features.Store.AccountUseCase;

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
}
