namespace mark.davison.spacetraders.shared.models.dtos.Queries.FetchAgent;

[GetRequest(Path = "fetch-agent-query")]
public sealed class FetchAgentQueryRequest : IdentifiedQueryRequest<FetchAgentQueryRequest, FetchAgentQueryResponse>
{
    public string AgentSymbol { get; set; } = string.Empty;
}
