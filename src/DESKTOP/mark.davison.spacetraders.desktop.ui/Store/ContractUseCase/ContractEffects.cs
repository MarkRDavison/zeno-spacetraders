namespace mark.davison.spacetraders.desktop.ui.Store.ContractUseCase;

[DesktopEffect]
public sealed class ContractEffects
{
    private readonly IClientHttpRepository _clientHttpRepository;

    public ContractEffects(
        IDesktopStateDispatcher dispatcher,
        IClientHttpRepository clientHttpRepository)
    {
        _clientHttpRepository = clientHttpRepository;

    }

    public async Task HandleFetchContractsActionAsync(FetchContractsAction action, IDesktopStateDispatcher dispatcher)
    {
        var queryRequest = new FetchContractsQueryRequest
        {
            Identifier = action.Identifier
        };

        var queryResponse = await _clientHttpRepository.Get<FetchContractsQueryResponse, FetchContractsQueryRequest>(queryRequest, CancellationToken.None);

        dispatcher.Dispatch(new UpdateContractsActionResponse
        {
            ActionId = action.ActionId,
            Errors = queryResponse.Errors,
            Warnings = queryResponse.Warnings,
            Value = queryResponse.Value
        });
    }
}
