namespace mark.davison.spacetraders.desktop.common.Ignition;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddSpacetradersCommon(this IServiceCollection services)
    {
        services
            .AddCommonDesktop()
            .Configure<OdicClientSettings>(_ =>
            {
                _.Authority = "https://keycloak.markdavison.kiwi/auth/realms/markdavison.kiwi";
                _.ClientId = "zeno-spacetraders-public";
                _.Scope = "openid profile email offline_access";
                _.PersistenceLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "spacetraders");
            });

        services.AddSingleton<IClientHttpRepository>(_ => _.GetRequiredService<IDesktopAuthenticationService>().GetAuthenticatedClient("https://localhost:50000"));

        return services;
    }
}