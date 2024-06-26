using mark.davison.spacetraders.shared.models.dtos.Commands.NegotiateContract;

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

    public async Task HandleNegotiateContractActionAsync(NegotiateContractAction action, IDesktopStateDispatcher dispatcher)
    {
        var request = new NegotiateContractCommandRequest
        {
            Identifier = action.Identifier,
            ShipSymbol = action.ShipSymbol
        };

        var response = await _clientHttpRepository.Post<NegotiateContractCommandResponse, NegotiateContractCommandRequest>(request, CancellationToken.None);

        dispatcher.Dispatch(new UpdateContractsActionResponse
        {
            ActionId = action.ActionId,
            Errors = response.Errors,
            Warnings = response.Warnings,
            Value = response.Value is null ? [] : [response.Value]
        });
    }
}
