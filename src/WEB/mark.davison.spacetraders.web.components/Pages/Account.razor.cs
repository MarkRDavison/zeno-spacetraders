namespace mark.davison.spacetraders.web.components.Pages;

public partial class Account
{
    [Parameter]
    public required string Identifier { get; set; }

    [Inject]
    public required IState<AgentState> AgentState { get; set; }

    [Inject]
    public required IStoreHelper StoreHelper { get; set; }

    private AgentDto? Agent => AgentState.Value.GetAgent(Identifier);

    protected override async Task OnParametersSetAsync()
    {
        if (Agent == null)
        {
            await StoreHelper.DispatchAndWaitForResponse<FetchAgentAction, UpdateAgentActionResponse>(new FetchAgentAction { Identifier = Identifier });
        }
    }
}
