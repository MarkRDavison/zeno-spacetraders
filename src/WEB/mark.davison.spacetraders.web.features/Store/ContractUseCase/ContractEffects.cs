using mark.davison.spacetraders.shared.models.dtos.Commands.AcceptContract;
using mark.davison.spacetraders.web.features.Store.AccountUseCase;

namespace mark.davison.spacetraders.web.features.Store.ContractUseCase;

public sealed class ContractEffects
{
    private readonly IClientHttpRepository _repository;

    public ContractEffects(IClientHttpRepository repository)
    {
        _repository = repository;
    }

    [EffectMethod]
    public async Task HandleFetchContractsActionAsync(FetchContractsAction action, IDispatcher dispatcher)
    {
        var commandRequest = new FetchContractsCommandRequest
        {
            AccountId = action.AccountId,
            Meta = action.Meta
        };

        var commandResponse = await _repository.Post<FetchContractsCommandResponse, FetchContractsCommandRequest>(commandRequest, CancellationToken.None);

        var actionResponse = new FetchContractsActionResponse
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
    public async Task HandleAcceptContractActionAsync(AcceptContractAction action, IDispatcher dispatcher)
    {
        var commandRequest = new AcceptContractCommandRequest
        {
            AccountId = action.AccountId,
            ContractId = action.ContractId
        };

        var commandResponse = await _repository.Post<AcceptContractCommandResponse, AcceptContractCommandRequest>(commandRequest, CancellationToken.None);

        var actionResponse = new AcceptContractActionResponse
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
}
