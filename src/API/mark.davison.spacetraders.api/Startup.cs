namespace mark.davison.spacetraders.api;

[UseCQRSServer(typeof(DtosRootType), typeof(CommandsRootType), typeof(QueriesRootType))]
public sealed class Startup(IConfiguration Configuration)
{
    public AppSettings AppSettings { get; set; } = null!;

    public void ConfigureServices(IServiceCollection services)
    {
        AppSettings = services.ConfigureSettingsServices<AppSettings>(Configuration);
        if (AppSettings == null) { throw new InvalidOperationException(); }

        Console.WriteLine(AppSettings.DumpAppSettings(AppSettings.PRODUCTION_MODE));

        services
            .AddCors()
            .AddLogging()
            .AddJwtAuth(AppSettings.AUTH)
            .AddAuthorization()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddHttpContextAccessor()
            .AddHealthCheckServices<InitializationHostedService>()
            .AddScoped<ICurrentUserContext, CurrentUserContext>()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddDatabase<SpacetradersDbContext>(AppSettings.PRODUCTION_MODE, AppSettings.DATABASE, typeof(SqliteContextFactory), typeof(PostgresContextFactory))
            .AddCoreDbContext<SpacetradersDbContext>()
            .AddScoped<ISpacetradersDbContext>(_ => _.GetRequiredService<SpacetradersDbContext>())
            .AddSingleton<IDateService>(new DateService(DateService.DateMode.Utc))
            .AddCQRSServer()
            .AddHttpClient()
            .AddHttpContextAccessor()
            .AddRedis(AppSettings.REDIS, AppSettings.SECTION, AppSettings.PRODUCTION_MODE);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors(builder =>
            builder
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                .SetIsOriginAllowed(_ => true) // TODO: Config driven
                .AllowAnyMethod()
                .AllowCredentials()
                .AllowAnyHeader());

        app.UseHttpsRedirection();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app
            .UseMiddleware<RequestResponseLoggingMiddleware>()
            .UseRouting()
            .UseAuthentication()
            .UseAuthorization()
            .UseMiddleware<PopulateUserContextMiddleware>()
            .UseMiddleware<ValidateUserExistsInDbMiddleware>() // TODO: To common
            .UseEndpoints(endpoints =>
            {
                endpoints
                    .MapGet<User>()
                    .MapGetById<User>()
                    .MapPost<User>()
                    .MapHealthChecks()
                    .MapCQRSEndpoints();
            });
    }
}
