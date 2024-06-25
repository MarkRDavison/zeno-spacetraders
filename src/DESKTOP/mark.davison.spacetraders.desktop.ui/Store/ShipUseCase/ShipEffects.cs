using mark.davison.spacetraders.shared.models.dtos.Commands.ModifyShipState;

namespace mark.davison.spacetraders.desktop.ui.Store.ShipUseCase;

[DesktopEffect]
public sealed class ShipEffects
{
    private readonly IClientHttpRepository _clientHttpRepository;

    public ShipEffects(
        IDesktopStateDispatcher dispatcher,
        IClientHttpRepository clientHttpRepository)
    {
        _clientHttpRepository = clientHttpRepository;

    }

    public async Task HandleFetchShipsActionAsync(FetchShipsAction action, IDesktopStateDispatcher dispatcher)
    {
        var queryRequest = new FetchShipsQueryRequest
        {
            Identifier = action.Identifier
        };

        var queryResponse = await _clientHttpRepository.Get<FetchShipsQueryResponse, FetchShipsQueryRequest>(queryRequest, CancellationToken.None);

        dispatcher.Dispatch(new UpdateShipsActionResponse
        {
            ActionId = action.ActionId,
            Errors = queryResponse.Errors,
            Warnings = queryResponse.Warnings,
            Value = queryResponse.Value
        });
    }

    public async Task HandleModifyShipStateActionAsync(ModifyShipStateAction action, IDesktopStateDispatcher dispatcher)
    {
        var commandRequest = new ModifyShipStateCommandRequest
        {
            Identifier = action.Identifier,
            ShipSymbol = action.ShipSymbol,
            SetDocked = action.SetDocked,
            SetOrbiting = action.SetOrbiting
        };

        var commandResponse = await _clientHttpRepository.Post<ModifyShipStateCommandResponse, ModifyShipStateCommandRequest>(commandRequest, CancellationToken.None);

        dispatcher.Dispatch(new UpdateShipsActionResponse
        {
            ActionId = action.ActionId,
            Errors = commandResponse.Errors,
            Warnings = commandResponse.Warnings,
            Value = commandResponse.Value is null ? [] : [commandResponse.Value]
        });
    }
}
