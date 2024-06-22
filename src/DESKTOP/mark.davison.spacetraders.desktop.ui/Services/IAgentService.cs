namespace mark.davison.spacetraders.desktop.ui.Services;

public interface IAgentService
{
    Task UpdateMyAgentAsync(CancellationToken cancellationToken = default);
    Task UpdateAgentAsync(string agentSymbol, CancellationToken cancellationToken = default);
}
