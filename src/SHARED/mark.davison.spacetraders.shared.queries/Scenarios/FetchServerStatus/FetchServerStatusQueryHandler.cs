namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchServerStatus;

public sealed class FetchServerStatusQueryHandler(
    IQueryProcessor<FetchServerStatusQueryRequest, FetchServerStatusQueryResponse> processor
) : ValidateAndProcessQueryHandler<FetchServerStatusQueryRequest, FetchServerStatusQueryResponse>(
    processor)
{
}
