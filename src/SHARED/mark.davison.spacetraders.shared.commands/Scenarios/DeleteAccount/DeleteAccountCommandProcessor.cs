namespace mark.davison.spacetraders.shared.commands.Scenarios.DeleteAccount;

public sealed class DeleteAccountCommandProcessor : ICommandProcessor<DeleteAccountCommandRequest, DeleteAccountCommandResponse>
{
    private readonly ISpacetradersDbContext _dbContext;

    public DeleteAccountCommandProcessor(ISpacetradersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DeleteAccountCommandResponse> ProcessAsync(DeleteAccountCommandRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        var account = await _dbContext.Set<Account>().Where(_ => _.Id == request.AccountId && _.UserId == currentUserContext.CurrentUser.Id).SingleAsync(cancellationToken);

        if (account == null)
        {
            return ValidationMessages.CreateErrorResponse<DeleteAccountCommandResponse>(
                ValidationMessages.FAILED_TO_FIND_ENTITY,
                nameof(Account));
        }

        await _dbContext.DeleteEntityAsync(account, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new();
    }
}
