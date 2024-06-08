namespace mark.davison.spacetraders.web.services.Ignition;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountContextService, AccountContextService>();
        return services;
    }
}
