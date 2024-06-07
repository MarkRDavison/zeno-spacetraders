namespace mark.davison.spacetraders.avalonia.ui.Services;

public interface IAgentInfoPageViewModelService
{
    public Task<Agent> GetAgentAsync(CancellationToken cancellationToken);
}
