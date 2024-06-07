using mark.davison.spacetraders.core.Ignition;

namespace mark.davison.spacetraders.api;

public class Startup
{
    public IConfiguration Configuration { get; }


    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSpaceTradersCore();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();

        app.UseEndpoints(_ =>
        {
            _.UseAgentRoutes();
        });
    }
}
