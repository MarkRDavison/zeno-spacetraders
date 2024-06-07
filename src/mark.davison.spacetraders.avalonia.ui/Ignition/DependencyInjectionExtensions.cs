namespace mark.davison.spacetraders.avalonia.ui.Ignition;

public static class DependencyInjectionExtensions
{
    static DependencyInjectionExtensions()
    {
        var services = new ServiceCollection();

        services.AddSpacetradersUi();

        DesignTimeServiceProvider = services.BuildServiceProvider();
    }

    public static IServiceProvider DesignTimeServiceProvider { get; }

    public static IServiceCollection AddSpacetradersUi(this IServiceCollection services)
    {
        services.AddLogging();
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<LandingPageViewModel>();
        services.AddTransient<RegisterViewModel>();
        services.AddTransient<SignInViewModel>();
        services.AddTransient<AgentInfoPageViewModel>();
        services.AddTransient<MainAuthenticatedLayoutViewModel>();
        services.AddTransient<ShipsInfoPageViewModel>();
        services.AddTransient<ShipPageViewModel>();
        services.AddTransient<WaypointsInfoPageViewModel>();

        services.AddTransient<IWaypointService, WaypointService>();
        services.AddTransient<IShipService, ShipService>();
        services.AddTransient<IRegisterViewModelService, RegisterViewModelService>();
        services.AddTransient<IAgentInfoPageViewModelService, AgentInfoPageViewModelService>();
        services.AddSingleton<IApplicationNotificationService, ApplicationNotificationService>();
        services.AddSingleton<IAccountContext, AccountContext>();
        services.AddSingleton<IPersistentState, PersistentState>();
        services.AddSingleton<IAuthenticationContext, AuthenticationContext>();
        services.AddSingleton<ISpaceTradersApiClient>(_ =>
        {
            return new SpaceTradersApiClient(_.GetRequiredService<IAuthenticationContext>(), new HttpClient());
        });

        return services;
    }
}
