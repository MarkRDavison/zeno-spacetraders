namespace mark.davison.spacetraders.shared.models.dtos.Shared;

public sealed class CooldownDto
{
    public int TotalSeconds { get; set; }
    public int RemainingSeconds { get; set; }
    public DateTimeOffset Expiration { get; set; }
}
