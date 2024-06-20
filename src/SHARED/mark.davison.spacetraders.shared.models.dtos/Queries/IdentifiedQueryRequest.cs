namespace mark.davison.spacetraders.shared.models.dtos.Queries;

public abstract class IdentifiedQueryRequest<TQuery, TResponse> : IQuery<TQuery, TResponse>
    where TQuery : class
    where TResponse : class, new()
{
    public string Identifier { get; set; } = string.Empty;
}

public abstract class PaginatedIdentifiedQueryRequest<TQuery, TResponse> : IdentifiedQueryRequest<TQuery, TResponse>
    where TQuery : class
    where TResponse : class, new()
{
    public int? Page { get; set; }
    public int? Limit { get; set; }
}
