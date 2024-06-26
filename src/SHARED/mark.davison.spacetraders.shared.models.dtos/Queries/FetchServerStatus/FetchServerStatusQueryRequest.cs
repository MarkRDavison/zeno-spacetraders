namespace mark.davison.spacetraders.shared.models.dtos.Queries.FetchServerStatus;

[GetRequest(Path = "fetch-server-status-query")]
public sealed class FetchServerStatusQueryRequest : IQuery<FetchServerStatusQueryRequest, FetchServerStatusQueryResponse>
{
}
