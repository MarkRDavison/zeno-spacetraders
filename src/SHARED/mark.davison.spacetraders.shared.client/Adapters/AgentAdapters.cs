namespace mark.davison.spacetraders.shared.client.Adapters;

public static class AgentAdapters
{
    public static AgentDto Adapt(this Agent agent)
    {
        return new AgentDto
        {
            Symbol = agent.Symbol,
            Headquarters = agent.Headquarters,
            Credits = agent.Credits,
            ShipCount = agent.ShipCount
        };
    }
}
