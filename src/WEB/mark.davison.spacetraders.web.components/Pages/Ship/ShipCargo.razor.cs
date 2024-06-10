namespace mark.davison.spacetraders.web.components.Pages.Ship;

public partial class ShipCargo
{
    [Parameter, EditorRequired]
    public required ShipDto ShipDto { get; set; }

    [Inject]
    public required IState<ShipState> ShipState { get; set; }

    [Inject]
    public required IAccountContextService AccountContextService { get; set; }

    [Inject]
    public required IStoreHelper StoreHelper { get; set; }

    public ShipCargoDto? CargoDto => ShipState.Value.Cargo.FirstOrDefault(_ => _.ShipSymbol == ShipDto.Symbol);

    protected override async Task OnParametersSetAsync()
    {
        if (CargoDto == null && AccountContextService.GetActiveAccount() is { } account)
        {
            await StoreHelper.DispatchAndWaitForResponse<FetchShipCargoAction, FetchShipCargoActionResponse>(new()
            {
                AccountId = account.Id,
                ShipSymbol = ShipDto.Symbol
            });
        }
    }
}
