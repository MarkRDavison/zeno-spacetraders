namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchServerStatus;

public sealed class FetchServerStatusQueryProcessor : IQueryProcessor<FetchServerStatusQueryRequest, FetchServerStatusQueryResponse>
{
    private readonly ISpacetradersApiClient _apiClient;

    public FetchServerStatusQueryProcessor(ISpacetradersApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<FetchServerStatusQueryResponse> ProcessAsync(FetchServerStatusQueryRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        var apiResponse = await _apiClient.GetStatusAsync(cancellationToken);

        return new FetchServerStatusQueryResponse
        {
            Value = new ServerStatusDto
            {
                Status = apiResponse.Status,
                Version = apiResponse.Version,
                ResetDate = apiResponse.ResetDate,
                ResetFrequency = apiResponse.ServerResets.Frequency,
                NextResetDate = apiResponse.ServerResets.Next
            }
        };
    }
}
