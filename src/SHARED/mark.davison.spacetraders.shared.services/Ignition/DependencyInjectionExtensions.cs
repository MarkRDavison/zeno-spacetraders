namespace mark.davison.spacetraders.shared.services.Ignition;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationContext, AuthenticationContext>();
        services.AddScoped<ISpaceTradersApiClient, SpaceTradersApiClient>(_ =>
        {
            return new SpaceTradersApiClient(
                _.GetRequiredService<IAuthenticationContext>(),
                _.GetRequiredService<IHttpClientFactory>().CreateClient("SPACETRADERS"));
        });

        return services;
    }
}
