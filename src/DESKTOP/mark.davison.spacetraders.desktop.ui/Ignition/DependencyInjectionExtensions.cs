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
            .AddSingleton<IContractService, ContractService>()
            .AddTransient<IFormSubmission<RegisterAgentDialogViewModel>, RegisterAgentDialogFormSubmission>()
            .AddSingleton(typeof(IState<>), typeof(StateImplementation<>))
            .AddSingleton<IStoreHelper, DesktopStoreHelper>()
            .AddSingleton<IDesktopStateDispatcher, DesktopStateDispatcher>()
            .AddSingleton<IDesktopActionSubscriber, DesktopActionSubscriber>()
            .AddSingleton<StateReducer<ShipState>, ShipReducers>()
            .AddSingleton<StateReducer, ShipReducers>()
            .AddSingleton<StateEffects, ShipEffects>();

        return services;
    }
}
