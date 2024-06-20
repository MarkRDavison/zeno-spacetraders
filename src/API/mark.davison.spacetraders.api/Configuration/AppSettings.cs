namespace mark.davison.spacetraders.api.Configuration;

public sealed class AppSettings : IAppSettings
{
    public string SECTION => "SPACETRADERS";
    public List<AuthAppSettings> AUTH { get; set; } = new();
    public DatabaseAppSettings DATABASE { get; set; } = new();
    public RedisAppSettings REDIS { get; set; } = new();
    public ClaimsAppSettings CLAIMS { get; set; } = new();
    public bool PRODUCTION_MODE { get; set; }
}
