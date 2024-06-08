namespace mark.davison.spacetraders.web.ui.Ignition;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddSpacetradersWeb(this IServiceCollection services, IAuthenticationConfig authConfig)
    {
        services
            .AddSpacetradersComponents(authConfig)
            .UseFluxorState(typeof(Program), typeof(FeaturesRootType))
            .UseClientRepository(WebConstants.ApiClientName, WebConstants.LocalBffRoot);

        return services;
    }
}
