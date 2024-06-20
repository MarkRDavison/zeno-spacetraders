namespace mark.davison.spacetraders.web.features.Store.AgentUseCase;

[FeatureState]
public sealed class AgentState
{
    public AgentDto? GetAgent(string symbol) => Agents.FirstOrDefault(_ => _.Symbol == symbol);
    public ReadOnlyCollection<AgentDto> Agents { get; }

    public AgentState() : this([])
    {

    }
    public AgentState(IEnumerable<AgentDto> agents)
    {
        Agents = new([.. agents]);
    }
}
