namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchContracts;

public sealed class FetchContractsQueryHandler(
    IQueryProcessor<FetchContractsQueryRequest, FetchContractsQueryResponse> processor
) : ValidateAndProcessQueryHandler<FetchContractsQueryRequest, FetchContractsQueryResponse>(
    processor)
{
}
