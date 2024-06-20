namespace mark.davison.spacetraders.web.features.Store;

public abstract class IdentifiedAction : BaseAction
{
    public string Identifier { get; set; } = string.Empty;
}

public abstract class PaginatedIdentifiedAction : IdentifiedAction
{
    public int? Page { get; set; }
    public int? Limit { get; set; }
}