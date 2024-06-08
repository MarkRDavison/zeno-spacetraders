namespace mark.davison.spacetraders.web.features.Store.ShipUseCase;

public sealed class FetchShipsAction : BaseAction
{
    public Guid AccountId { get; set; }
    public MetaInfo Meta { get; set; } = new();
}

public sealed class FetchShipsActionResponse : BaseActionResponse<List<ShipDto>>
{

}
