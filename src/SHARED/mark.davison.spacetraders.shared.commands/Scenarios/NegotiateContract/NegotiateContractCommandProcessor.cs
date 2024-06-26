namespace mark.davison.spacetraders.shared.commands.Scenarios.NegotiateContract;

public sealed class NegotiateContractCommandProcessor(
    ISpacetradersDbContext dbContext,
    ISpacetradersApiClient apiClient
) : IdentifiedCommandProcessor<NegotiateContractCommandRequest, NegotiateContractCommandResponse>(
    dbContext,
    apiClient)
{
    protected override async Task<NegotiateContractCommandResponse> ProcessAsyncIdentified(NegotiateContractCommandRequest request, ISpacetradersApiClient apiClient, Guid userId, CancellationToken cancellationToken)
    {
        var apiResponse = await apiClient.NegotiateContractAsync(request.ShipSymbol, cancellationToken);

        return new NegotiateContractCommandResponse
        {
            Value = apiResponse.Data.Contract.Adapt()
        };
    }
}
