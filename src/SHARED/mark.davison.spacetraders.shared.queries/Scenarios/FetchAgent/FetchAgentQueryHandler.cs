namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchAgent;

public sealed class FetchAgentQueryHandler(
    IQueryProcessor<FetchAgentQueryRequest, FetchAgentQueryResponse> processor
) : ValidateAndProcessQueryHandler<FetchAgentQueryRequest, FetchAgentQueryResponse>(
    processor)
{
}
