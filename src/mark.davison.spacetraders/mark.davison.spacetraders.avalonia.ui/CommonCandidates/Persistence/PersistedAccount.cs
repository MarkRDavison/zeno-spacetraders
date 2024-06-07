namespace mark.davison.spacetraders.avalonia.ui.CommonCandidates.Persistence;

public sealed class PersistedAccount
{
    public string Token { get; set; } = string.Empty;
    public string Identifier { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public DateOnly ResetDate { get; set; }
}
