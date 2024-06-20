namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchContract;

public sealed class FetchContractQueryProcessor(
    ISpacetradersDbContext dbContext,
    ISpaceTradersApiClient apiClient
) : IdentifiedQueryProcessor<FetchContractQueryRequest, FetchContractQueryResponse>(
    dbContext,
    apiClient)
{
    protected override async Task<FetchContractQueryResponse> ProcessAsyncIdentified(FetchContractQueryRequest request, ISpaceTradersApiClient apiClient, Guid userId, CancellationToken cancellationToken)
    {
        var apiResponse = await apiClient.GetContractAsync(request.ContractId, cancellationToken);

        return new FetchContractQueryResponse
        {
            Value = ContractHelpers.ToContractDto(apiResponse.Data)
        };
    }
}
