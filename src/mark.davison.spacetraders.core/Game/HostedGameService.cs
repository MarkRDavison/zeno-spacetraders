namespace mark.davison.spacetraders.core.Game;

public class HostedGameService : IHostedGameService
{
    private readonly ILogger<HostedGameService> _logger;
    private readonly IGameOrchestrationService _gameOrchestrationService;

    public HostedGameService(
        ILogger<HostedGameService> logger,
        IGameOrchestrationService gameOrchestrationService
    )
    {
        _logger = logger;
        _gameOrchestrationService = gameOrchestrationService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _gameOrchestrationService.Initialise();
        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}
