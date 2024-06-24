namespace mark.davison.spacetraders.shared.commands.Scenarios.DeleteAgent;

public sealed class DeleteAgentCommandProcessor : ICommandProcessor<DeleteAgentCommandRequest, DeleteAgentCommandResponse>
{
    private readonly ISpacetradersDbContext _dbContext;

    public DeleteAgentCommandProcessor(ISpacetradersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DeleteAgentCommandResponse> ProcessAsync(DeleteAgentCommandRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        var response = await _dbContext.DeleteEntityByIdAsync<Account>(request.AccountId, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        if (response is null)
        {
            return ValidationMessages.CreateErrorResponse<DeleteAgentCommandResponse>(
                ValidationMessages.FAILED_TO_FIND_ENTITY,
                nameof(Account),
                nameof(DeleteAgentCommandRequest.AccountId));
        }

        return new();
    }
}
