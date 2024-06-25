namespace mark.davison.spacetraders.shared.commands.Scenarios.RegisterAgent;

public sealed class RegisterAgentCommandProcessor : ICommandProcessor<RegisterAgentCommandRequest, RegisterAgentCommandResponse>
{
    private readonly ISpacetradersDbContext _dbContext;
    private readonly ISpacetradersApiClient _apiClient;

    public RegisterAgentCommandProcessor(
        ISpacetradersDbContext dbContext,
        ISpacetradersApiClient apiClient)
    {
        _dbContext = dbContext;
        _apiClient = apiClient;
    }

    public async Task<RegisterAgentCommandResponse> ProcessAsync(RegisterAgentCommandRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        try
        {
            var apiResponse = await _apiClient.RegisterAsync(new()
            {
                Symbol = request.Identifier,
                Faction = Enum.Parse<FactionSymbol>(request.Faction, true),
                Email = request.Email ?? string.Empty
            }, cancellationToken);

            var versionResponse = await _apiClient.GetStatusAsync(cancellationToken);

            var account = new Account
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                UserId = currentUserContext.CurrentUser.Id,
                Token = apiResponse.Data.Token,
                Identifier = apiResponse.Data.Agent.Symbol,
                Version = versionResponse.Version
            };

            await _dbContext.AddAsync(account, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new RegisterAgentCommandResponse
            {
                Account = new AccountDto
                {
                    Id = account.Id,
                    Identifier = account.Identifier,
                    Email = account.Email,
                    Version = account.Version
                },
                Value = apiResponse.Data.Agent.Adapt()
            };
        }
        catch (SpacetradersApiException spaceException)
        {
            return new RegisterAgentCommandResponse
            {
                Errors = [spaceException.Response
                    ?? spaceException.InnerException?.Message
                    ?? spaceException.StatusCode.ToString()]
            };
        }
    }
}
