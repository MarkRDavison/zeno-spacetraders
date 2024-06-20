namespace mark.davison.spacetraders.web.components.Pages.Waypoint;

public partial class Marketplace
{
    [Parameter, EditorRequired]
    public required string Identifier { get; set; }

    [Parameter, EditorRequired]
    public required MarketplaceDto? MarketplaceDto { get; set; }
}
