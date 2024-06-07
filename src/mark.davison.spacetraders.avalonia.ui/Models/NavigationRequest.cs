namespace mark.davison.spacetraders.avalonia.ui.Models;

public enum NavigationPage
{
    Ships
}

public sealed class NavigationRequest
{
    public NavigationPage Page { get; set; }
}
