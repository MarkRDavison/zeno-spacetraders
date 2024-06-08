using mark.davison.spacetraders.avalonia.ui.CommonCandidates.Grid;

namespace mark.davison.spacetraders.avalonia.ui.ViewModels.Authenticated;

public partial class ContractsInfoPageViewModel : AsyncViewModel
{
    public GridMeta GridMeta { get; } = new()
    {
        Limit = 15,
        Page = 1,
        Total = 50
    };
}
