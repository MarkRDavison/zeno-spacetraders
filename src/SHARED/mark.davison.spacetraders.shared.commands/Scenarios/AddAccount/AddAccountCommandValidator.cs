namespace mark.davison.spacetraders.shared.commands.Scenarios.AddAccount;

public sealed class AddAccountCommandValidator : ICommandValidator<AddAccountCommandRequest, AddAccountCommandResponse>
{
    private readonly ISpacetradersDbContext _dbContext;

    public AddAccountCommandValidator(ISpacetradersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AddAccountCommandResponse> ValidateAsync(AddAccountCommandRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        if (request.AddExisting)
        {
            if (string.IsNullOrEmpty(request.Token))
            {
                return ValidationMessages.CreateErrorResponse<AddAccountCommandResponse>(
                    ValidationMessages.INVALID_PROPERTY,
                    nameof(AddAccountCommandRequest),
                    nameof(AddAccountCommandRequest.Token));
            }

            var existing = await _dbContext
                .Set<Account>()
                .AsNoTracking()
                .Where(_ => _.Token == request.Token && _.UserId == currentUserContext.CurrentUser.Id)
                .CountAsync(cancellationToken);

            if (existing > 0)
            {
                return ValidationMessages.CreateErrorResponse<AddAccountCommandResponse>(
                    ValidationMessages.DUPLICATE_ENTITY,
                    nameof(Account),
                    nameof(Account.Token));
            }
        }
        else
        {
            if (string.IsNullOrEmpty(request.Identifier))
            {
                return ValidationMessages.CreateErrorResponse<AddAccountCommandResponse>(
                    ValidationMessages.INVALID_PROPERTY,
                    nameof(AddAccountCommandRequest),
                    nameof(AddAccountCommandRequest.Identifier));
            }

            var existing = await _dbContext
                .Set<Account>()
                .AsNoTracking()
                .Where(_ => _.Identifier == request.Identifier && _.UserId == currentUserContext.CurrentUser.Id)
                .CountAsync(cancellationToken);

            if (existing > 0)
            {
                return ValidationMessages.CreateErrorResponse<AddAccountCommandResponse>(
                    ValidationMessages.DUPLICATE_ENTITY,
                    nameof(Account),
                    nameof(Account.Identifier));
            }

            if (string.IsNullOrEmpty(request.FactionSymbol) ||
                !Enum.TryParse<FactionSymbol>(request.FactionSymbol, out var faction))
            {
                return ValidationMessages.CreateErrorResponse<AddAccountCommandResponse>(
                    ValidationMessages.INVALID_PROPERTY,
                    nameof(AddAccountCommandRequest),
                    nameof(AddAccountCommandRequest.FactionSymbol));
            }
        }

        return new();
    }
}
