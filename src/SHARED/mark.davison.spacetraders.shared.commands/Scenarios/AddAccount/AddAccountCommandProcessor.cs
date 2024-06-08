namespace mark.davison.spacetraders.shared.commands.Scenarios.AddAccount;

public sealed class AddAccountCommandProcessor : ICommandProcessor<AddAccountCommandRequest, AddAccountCommandResponse>
{
    private readonly ISpacetradersDbContext _dbContext;
    private readonly ISpaceTradersApiClient _spaceTradersApiClient;

    public AddAccountCommandProcessor(
        ISpacetradersDbContext dbContext,
        ISpaceTradersApiClient spaceTradersApiClient)
    {
        _dbContext = dbContext;
        _spaceTradersApiClient = spaceTradersApiClient;
    }

    public async Task<AddAccountCommandResponse> ProcessAsync(AddAccountCommandRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        if (!request.AddExisting)
        {
            var apiResponse = await _spaceTradersApiClient.RegisterAsync(
                new RegisterBody
                {
                    Email = request.Email,
                    Symbol = request.Identifier,
                    Faction = Enum.Parse<FactionSymbol>(request.FactionSymbol)
                },
                cancellationToken);

            request.Token = apiResponse.Data.Token;
        }

        var handler = new JwtSecurityTokenHandler();
        try
        {
            var jwt = handler.ReadJwtToken(request.Token);

            var identifier = jwt.Payload["identifier"] as string;
            var version = jwt.Payload["version"] as string;

            if (string.IsNullOrEmpty(identifier) || string.IsNullOrEmpty(version))
            {
                throw new InvalidOperationException("Invalid JWT token");
            }

            var account = new Account
            {
                Id = Guid.NewGuid(),
                Token = request.Token,
                Email = request.Email,
                UserId = currentUserContext.CurrentUser.Id,
                Version = version,
                Identifier = identifier
            };

            await _dbContext.UpsertEntityAsync(account, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new AddAccountCommandResponse
            {
                Value = new AccountDto
                {
                    Id = account.Id,
                    Email = account.Email,
                    Identifier = account.Identifier,
                    Version = account.Version
                }
            };
        }
        catch (Exception)
        {
            return ValidationMessages.CreateErrorResponse<AddAccountCommandResponse>(
                ValidationMessages.INVALID_PROPERTY,
                nameof(AddAccountCommandRequest),
                nameof(AddAccountCommandRequest.Token));
        }
    }
}
