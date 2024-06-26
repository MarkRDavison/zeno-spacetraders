namespace mark.davison.spacetraders.shared.models.dtos.Shared;

public sealed class ServerStatusDto
{
    public string Status { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string ResetDate { get; set; } = string.Empty;
    public string ResetFrequency { get; set; } = string.Empty;
    public string NextResetDate { get; set; } = string.Empty;

    public DateTimeOffset Reset => DateTime.Parse(ResetDate);
    public DateTime NextReset => DateTime.Parse(NextResetDate).ToLocalTime();
}
