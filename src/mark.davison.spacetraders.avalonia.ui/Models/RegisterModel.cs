namespace mark.davison.spacetraders.avalonia.ui.Models;

public sealed class RegisterModel
{
    public string Callsign { get; set; } = string.Empty;
    public FactionSymbol? Faction { get; set; }
}
