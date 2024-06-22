namespace mark.davison.spacetraders.desktop.ui.Services;

internal sealed class AgentService : IdentifiedService, IAgentService
{
    public AgentService(
        IClientHttpRepository clientHttpRepository,
        IAccountService accountService
    ) : base(
        clientHttpRepository,
        accountService)
    {
    }

    public Task UpdateAgentAsync(string agentSymbol, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateMyAgentAsync(CancellationToken cancellationToken = default)
    {
        var request = new FetchAgentQueryRequest();

        var response = await Get<FetchAgentQueryRequest, FetchAgentQueryResponse>(request, cancellationToken);

        if (response.SuccessWithValue)
        {
            _accountService.SetActiveAccountAgent(new AgentModel
            {
                Symbol = response.Value.Symbol,
                Headquarters = response.Value.Headquarters,
                Credits = response.Value.Credits,
                ShipCount = response.Value.ShipCount
            });
        }
    }
}
