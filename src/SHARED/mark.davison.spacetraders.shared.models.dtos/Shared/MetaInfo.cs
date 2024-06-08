namespace mark.davison.spacetraders.shared.models.dtos.Shared;

public sealed class MetaInfo
{
    public int Total { get; set; }
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 10;
}
