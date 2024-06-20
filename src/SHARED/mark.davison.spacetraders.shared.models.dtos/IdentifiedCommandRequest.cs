namespace mark.davison.spacetraders.shared.models.dtos;

public abstract class IdentifiedCommandRequest<TCommand, TResponse> : ICommand<TCommand, TResponse>
    where TCommand : class
    where TResponse : class, new()
{
    public string Identifier { get; set; } = string.Empty;
}

public abstract class PaginatedIdentifiedCommandRequest<TCommand, TResponse> : IdentifiedCommandRequest<TCommand, TResponse>
    where TCommand : class
    where TResponse : class, new()
{
    public int? Page { get; set; }
    public int? Limit { get; set; }
}
