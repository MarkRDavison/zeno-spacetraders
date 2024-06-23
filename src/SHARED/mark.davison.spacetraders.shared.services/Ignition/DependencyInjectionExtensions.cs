using Microsoft.Extensions.Logging;

namespace mark.davison.spacetraders.shared.services.Ignition;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        services.AddScoped<ISpacetradersApiClient, SpacetradersApiClient>(_ =>
        {
            var client = _.GetRequiredService<IHttpClientFactory>().CreateClient("SPACETRADERS");
            client.BaseAddress = new Uri("https://api.spacetraders.io/v2/"); // TODO: Config
            return new SpacetradersApiClient(client, _.GetRequiredService<ILogger<SpacetradersApiClient>>());
        });

        return services;
    }
}
