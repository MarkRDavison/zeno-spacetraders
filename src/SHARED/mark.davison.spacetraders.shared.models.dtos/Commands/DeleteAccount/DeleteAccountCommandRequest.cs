namespace mark.davison.spacetraders.shared.models.dtos.Commands.DeleteAccount;

[PostRequest(Path = "delete-account-request")]
public sealed class DeleteAccountCommandRequest : ICommand<DeleteAccountCommandRequest, DeleteAccountCommandResponse>
{
    public Guid AccountId { get; set; }
}
