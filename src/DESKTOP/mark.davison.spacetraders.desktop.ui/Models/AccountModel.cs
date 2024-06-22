namespace mark.davison.spacetraders.desktop.ui.Models;

public class AccountModel
{
    public Guid Id { get; set; }
    public string Identifier { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string? Email { get; set; }
}
