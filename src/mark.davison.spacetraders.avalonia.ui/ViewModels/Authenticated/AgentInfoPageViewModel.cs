namespace mark.davison.spacetraders.avalonia.ui.ViewModels.Authenticated;

public partial class AgentInfoPageViewModel : AsyncViewModel
{
    private readonly IAgentInfoPageViewModelService _agentInfoPageViewModelService;

    public AgentInfoPageViewModel() : this(
        DependencyInjectionExtensions.DesignTimeServiceProvider.GetRequiredService<IAgentInfoPageViewModelService>())
    {

    }

    public AgentInfoPageViewModel(IAgentInfoPageViewModelService agentInfoPageViewModelService)
    {
        _agentInfoPageViewModelService = agentInfoPageViewModelService;
    }

    protected override async Task InitializeAsync()
    {
        var agent = await _agentInfoPageViewModelService.GetAgentAsync(CancellationToken.None);

        Dispatcher.UIThread.Invoke(() => Agent = agent);
    }

    [ObservableProperty]
    private Agent? _agent;
}
