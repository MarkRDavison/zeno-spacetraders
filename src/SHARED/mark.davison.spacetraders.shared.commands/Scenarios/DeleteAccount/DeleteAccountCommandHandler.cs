namespace mark.davison.spacetraders.shared.commands.Scenarios.DeleteAccount;

public sealed class DeleteAccountCommandHandler(
    ICommandProcessor<DeleteAccountCommandRequest, DeleteAccountCommandResponse> processor
) : ValidateAndProcessCommandHandler<DeleteAccountCommandRequest, DeleteAccountCommandResponse>(
    processor)
{
}
