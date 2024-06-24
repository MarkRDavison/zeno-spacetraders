namespace mark.davison.spacetraders.desktop.ui.Ignition;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddSpacetradersDesktop(this IServiceCollection services)
    {
        services
            .AddSingleton<AccountsPageViewModel>()
            .AddSingleton<ContractsPageViewModel>()
            .AddSingleton<ShipsPageViewModel>()
            .AddSingleton<WaypointsPageViewModel>()
            .AddSingleton<AgentSummaryViewModel>()
            .AddSingleton<IApplicationNotificationService, ApplicationNotificationService>()
            .AddSingleton<IAccountService, AccountService>()
            .AddSingleton<IAgentService, AgentService>()
            .AddTransient<IFormSubmission<RegisterAgentDialogViewModel>, RegisterAgentDialogFormSubmission>();

        services
            .AddSingleton<IStoreHelper, DesktopStoreHelper>()
            .AddSingleton<IDesktopStateDispatcher, DesktopStateDispatcher>()
            .AddSingleton<IDesktopActionSubscriber, DesktopActionSubscriber>()
            .AddDesktopState();

        return services;
    }
}
