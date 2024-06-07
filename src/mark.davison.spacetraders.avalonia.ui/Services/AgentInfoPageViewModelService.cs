namespace mark.davison.spacetraders.avalonia.ui.Services;

public sealed class AgentInfoPageViewModelService : IAgentInfoPageViewModelService
{
    private readonly ISpaceTradersApiClient _apiClient;
    private Agent? _agent = null; // TODO: Scoped service per login/user, new scope when changing account

    public AgentInfoPageViewModelService(ISpaceTradersApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<Agent> GetAgentAsync(CancellationToken cancellationToken)
    {
        if (_agent != null)
        {
            return _agent;
        }

        var response = await _apiClient.GetMyAgentAsync(cancellationToken);

        _agent = response.Data;

        return _agent;
    }
}
