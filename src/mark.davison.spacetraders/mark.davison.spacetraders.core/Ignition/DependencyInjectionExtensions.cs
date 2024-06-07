namespace mark.davison.spacetraders.core.Ignition;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddSpaceTradersCore(this IServiceCollection services)
    {
        services.AddSingleton<IAuthenticationContext, AuthenticationContext>();
        services.AddScoped<ISpaceTradersApiClient, SpaceTradersApiClient>(_ =>
        {
            return new SpaceTradersApiClient(_.GetRequiredService<IAuthenticationContext>(), new HttpClient());
        });
        services.AddSingleton<IGameOrchestrationService, GameOrchestrationService>();
        services.AddHostedService<HostedGameService>();

        return services;
    }
}
