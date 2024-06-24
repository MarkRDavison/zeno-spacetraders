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
}
