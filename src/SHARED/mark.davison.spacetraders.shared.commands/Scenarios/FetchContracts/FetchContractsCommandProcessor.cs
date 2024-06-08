namespace mark.davison.spacetraders.shared.commands.Scenarios.FetchContracts;

public sealed class FetchContractsCommandProcessor : ICommandProcessor<FetchContractsCommandRequest, FetchContractsCommandResponse>
{
    private readonly ISpacetradersDbContext _dbContext;
    private readonly ISpaceTradersApiClient _apiClient;

    public FetchContractsCommandProcessor(
        ISpacetradersDbContext dbContext,
        ISpaceTradersApiClient apiClient)
    {
        _dbContext = dbContext;
        _apiClient = apiClient;
    }

    public async Task<FetchContractsCommandResponse> ProcessAsync(FetchContractsCommandRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        var account = await _dbContext.GetByIdAsync<Account>(request.AccountId, cancellationToken);

        if (account == null)
        {
            return ValidationMessages.CreateErrorResponse<FetchContractsCommandResponse>(
                ValidationMessages.INVALID_PROPERTY,
                nameof(FetchContractsCommandRequest.AccountId));
        }

        _apiClient.Token = account.Token;

        var meta = request.Meta ?? new();

        var apiResponse = await _apiClient.GetContractsAsync(meta.Page, meta.Limit, cancellationToken);

        return new FetchContractsCommandResponse
        {
            Value = [.. apiResponse.Data.Select(FromContract)],
            Meta = new MetaInfo
            {
                Limit = apiResponse.Meta.Limit,
                Page = apiResponse.Meta.Page,
                Total = apiResponse.Meta.Total
            }
        };
    }

    private ContractDto FromContract(Contract contract)
    {
        return new ContractDto
        {
            Id = contract.Id,
            Accepted = contract.Accepted,
            DeadlineToAccept = contract.DeadlineToAccept,
            FactionSymbol = contract.FactionSymbol,
            Fulfilled = contract.Fulfilled,
            Type = contract.Type.ToString(),
            Terms = new ContractTermsDto
            {
                Payment = new ContractPaymentDto
                {
                    AcceptedCredits = contract.Terms.Payment.OnAccepted,
                    FulfilledCredits = contract.Terms.Payment.OnFulfilled
                }
            }
        };
    }
}