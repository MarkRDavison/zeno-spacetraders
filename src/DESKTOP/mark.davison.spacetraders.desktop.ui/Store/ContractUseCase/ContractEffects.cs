using mark.davison.spacetraders.shared.models.dtos.Commands.AcceptContract;
using mark.davison.spacetraders.shared.models.dtos.Commands.NegotiateContract;

namespace mark.davison.spacetraders.desktop.ui.Store.ContractUseCase;

[DesktopEffect]
public sealed class ContractEffects
{
    private readonly IClientHttpRepository _clientHttpRepository;
    private readonly IAccountService _accountService;

    public ContractEffects(
        IDesktopStateDispatcher dispatcher,
        IClientHttpRepository clientHttpRepository,
        IAccountService accountService)
    {
        _clientHttpRepository = clientHttpRepository;
        _accountService = accountService;
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

    public async Task HandleAcceptContractActionAsync(AcceptContractAction action, IDesktopStateDispatcher dispatcher)
    {
        var request = new AcceptContractCommandRequest
        {
            Identifier = action.Identifier,
            ContractId = action.ContractId
        };

        var response = await _clientHttpRepository.Post<AcceptContractCommandResponse, AcceptContractCommandRequest>(request, CancellationToken.None);

        dispatcher.Dispatch(new UpdateContractsActionResponse
        {
            ActionId = action.ActionId,
            Errors = response.Errors,
            Warnings = response.Warnings,
            Value = response.Value is null ? [] : [response.Value]
        });

        if (response.SuccessWithValue && response.Agent is not null)
        {
            _accountService.SetActiveAccountAgent(response.Agent);
        }
    }
}
