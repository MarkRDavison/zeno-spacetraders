namespace mark.davison.spacetraders.shared.models.dtos.Shared;

public sealed class AccountDto
{
    public Guid Id { get; set; }
    public string Identifier { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string? Email { get; set; }
}
