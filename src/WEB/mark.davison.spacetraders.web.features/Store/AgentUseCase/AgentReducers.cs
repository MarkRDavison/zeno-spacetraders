namespace mark.davison.spacetraders.web.features.Store.AgentUseCase;

public static class AgentReducers
{
    [ReducerMethod]
    public static AgentState UpdateAgentActionResponse(AgentState state, UpdateAgentActionResponse response)
    {
        if (response.SuccessWithValue)
        {
            return new AgentState([
                .. state.Agents.Where(_ => _.Symbol != response.Value.Symbol),
                response.Value]);
        }

        return state;
    }
}
