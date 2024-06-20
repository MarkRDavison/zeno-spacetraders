namespace mark.davison.spacetraders.shared.models.Helpers;

public static class AgentHelpers
{
    public static AgentDto ToAgentDto(Agent agent)
    {
        return new AgentDto
        {
            Symbol = agent.Symbol,
            Credits = agent.Credits,
            Headquarters = agent.Headquarters,
            ShipCount = agent.ShipCount,
            StartingFaction = agent.StartingFaction
        };
    }
}
