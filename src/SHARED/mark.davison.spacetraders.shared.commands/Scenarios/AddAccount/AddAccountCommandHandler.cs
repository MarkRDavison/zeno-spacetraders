namespace mark.davison.spacetraders.shared.commands.Scenarios.AddAccount;

public sealed class AddAccountCommandHandler : ValidateAndProcessCommandHandler<AddAccountCommandRequest, AddAccountCommandResponse>
{
    public AddAccountCommandHandler(
        ICommandProcessor<AddAccountCommandRequest, AddAccountCommandResponse> processor,
        ICommandValidator<AddAccountCommandRequest, AddAccountCommandResponse> validator
    ) : base(
        processor,
        validator)
    {
    }
}
