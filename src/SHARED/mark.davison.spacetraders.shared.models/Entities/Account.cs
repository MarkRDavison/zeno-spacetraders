namespace mark.davison.spacetraders.shared.models.Entities;

public class Account : SpaceTradersEntity
{
    public string Token { get; set; } = string.Empty;
    public string Identifier { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string? Email { get; set; }
}
