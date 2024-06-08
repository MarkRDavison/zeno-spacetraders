namespace mark.davison.spacetraders.avalonia.ui.CommonCandidates.Grid;

public class GridMeta
{
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 10;
    public int Total { get; set; }

    public int MaxPages => (int)Math.Ceiling((decimal)Total / (decimal)Limit);
}
