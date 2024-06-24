namespace mark.davison.spacetraders.shared.models.dtos.Commands.DeleteAgent;

[PostRequest(Path = "delete-agent-command")]
public sealed class DeleteAgentCommandRequest : ICommand<DeleteAgentCommandRequest, DeleteAgentCommandResponse>
{
    public Guid AccountId { get; set; }
}
