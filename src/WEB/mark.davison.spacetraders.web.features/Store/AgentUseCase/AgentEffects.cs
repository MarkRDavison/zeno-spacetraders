namespace mark.davison.spacetraders.web.features.Store.AgentUseCase;

public sealed class AgentEffects
{
    private readonly IClientHttpRepository _repository;

    public AgentEffects(IClientHttpRepository repository)
    {
        _repository = repository;
    }

    [EffectMethod]
    public async Task HandleUpdateAgentActionAsync(FetchAgentAction action, IDispatcher dispatcher)
    {
        var queryRequest = new FetchAgentQueryRequest
        {
            Identifier = action.Identifier
        };

        var queryResponse = await _repository.Get<FetchAgentQueryResponse, FetchAgentQueryRequest>(queryRequest, CancellationToken.None);

        var actionResponse = new UpdateAgentActionResponse
        {
            ActionId = action.ActionId,
            Errors = [.. queryResponse.Errors],
            Warnings = [.. queryResponse.Warnings],
            Value = queryResponse.Value
        };

        // TODO: Framework to dispatch general ***something went wrong***

        dispatcher.Dispatch(actionResponse);

    }
}