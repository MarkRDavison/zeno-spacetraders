namespace mark.davison.spacetraders.shared.models.dtos.Commands.AddAccount;

[PostRequest(Path = "add-account-command")]
public sealed class AddAccountCommandRequest : ICommand<AddAccountCommandRequest, AddAccountCommandResponse>
{
    public bool AddExisting { get; set; }
    public string Token { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Identifier { get; set; } = string.Empty;
    public string FactionSymbol { get; set; } = string.Empty;
}
