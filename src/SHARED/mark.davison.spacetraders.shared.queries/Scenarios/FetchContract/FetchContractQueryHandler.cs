namespace mark.davison.spacetraders.shared.queries.Scenarios.FetchContract;

public sealed class FetchContractQueryHandler(
    IQueryProcessor<FetchContractQueryRequest, FetchContractQueryResponse> processor
) : ValidateAndProcessQueryHandler<FetchContractQueryRequest, FetchContractQueryResponse>(
    processor)
{
}