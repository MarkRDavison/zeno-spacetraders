using mark.davison.spacetraders.shared.models.dtos.Queries.FetchShips;

namespace mark.davison.spacetraders.desktop.ui.Store.ShipUseCase;

public sealed class ShipEffects : StateEffects
{
    private readonly IClientHttpRepository _clientHttpRepository;
    private readonly IAccountService _accountService;

    public ShipEffects(
        IDesktopStateDispatcher dispatcher,
        IClientHttpRepository clientHttpRepository,
        IAccountService accountService
    ) : base(dispatcher)
    {
        _clientHttpRepository = clientHttpRepository;

        RegisterAction<FetchShipsAction>(HandleFetchShipsActionAsync);
        _accountService = accountService;
    }


    public async Task HandleFetchShipsActionAsync(FetchShipsAction action, IDesktopStateDispatcher dispatcher)
    {
        if (!_accountService.HasActiveAccount)
        {
            throw new InvalidOperationException(); // TODO: Frameworkitize
        }
        var queryRequest = new FetchShipsQueryRequest
        {
            Identifier = _accountService.GetActiveAccount().Identifier
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
