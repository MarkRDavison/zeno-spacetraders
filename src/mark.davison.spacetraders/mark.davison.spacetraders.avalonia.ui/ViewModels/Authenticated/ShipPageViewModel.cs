namespace mark.davison.spacetraders.avalonia.ui.ViewModels.Authenticated;

public partial class ShipPageViewModel : AsyncViewModel
{
    private readonly IShipService _shipService;
    private readonly IApplicationNotificationService _applicationNotificationService;

    public ShipPageViewModel(
        IShipService shipService,
        IApplicationNotificationService applicationNotificationService)
    {
        _shipService = shipService;
        _applicationNotificationService = applicationNotificationService;
    }

    protected override async Task InitializeAsync()
    {
        Ship = await _shipService.GetShipAsync(Symbol, CancellationToken.None);
    }

    public string Symbol { get; set; } = string.Empty;

    [RelayCommand]
    private void Back()
    {
        _applicationNotificationService.OnNavigationRequest(new NavigationRequest
        {
            Page = NavigationPage.Ships
        });
    }

    [ObservableProperty]
    private Ship? _ship;
}
