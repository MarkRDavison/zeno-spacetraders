namespace mark.davison.spacetraders.web.components.Ignition;

public static class DependecyInjectionExtensions
{
    public static IServiceCollection AddSpacetradersComponents(
        this IServiceCollection services,
        IAuthenticationConfig authConfig)
    {
        services.UseCommonClient(authConfig, typeof(Routes));
        services.AddWebServices();
        return services;
    }
}
