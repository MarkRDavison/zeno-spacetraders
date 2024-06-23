namespace mark.davison.spacetraders.desktop.ui.Store;

public abstract class IdentifiedBaseAction : BaseAction
{
    public string Identifier { get; set; } = string.Empty;
}
