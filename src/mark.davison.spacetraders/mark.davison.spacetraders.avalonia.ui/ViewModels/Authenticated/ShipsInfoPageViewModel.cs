namespace mark.davison.spacetraders.avalonia.ui.ViewModels.Authenticated;

public partial class ShipsInfoPageViewModel : AsyncViewModel, IDisposable
{
    private bool disposedValue;
    private readonly IShipService _shipService;
    private readonly IApplicationNotificationService _applicationNotificationService;

    public ShipsInfoPageViewModel(
        IShipService shipService,
        IApplicationNotificationService applicationNotificationService)
    {
        _shipService = shipService;
        _applicationNotificationService = applicationNotificationService;

        SetupNotifications();
    }

    private void SetupNotifications()
    {
        _applicationNotificationService.NavigationRequest += OnNavigationRequest;
    }

    private void TeardownNotifications()
    {
        _applicationNotificationService.NavigationRequest -= OnNavigationRequest;
    }

    private void OnNavigationRequest(object? sender, NavigationRequest e)
    {
        if (e.Page == NavigationPage.Ships)
        {
            // TODO: Dispose
            ShipPageViewModel = null;
        }
    }

    protected override async Task InitializeAsync()
    {
        var ships = await _shipService.GetShipsAsync(CancellationToken.None);

        Dispatcher.UIThread.Invoke(() =>
        {
            Ships = [.. ships];
        });
    }

    [RelayCommand]
    private void EditShip(string shipSymbol)
    {
        // TODO: DI
        ShipPageViewModel = new ShipPageViewModel(
            _shipService,
            _applicationNotificationService)
        {
            Symbol = shipSymbol
        };

        ShipPageViewModel.Init();
    }

    [ObservableProperty]
    private ObservableCollection<Ship> _ships = [];

    [ObservableProperty]
    private ShipPageViewModel? _shipPageViewModel;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                TeardownNotifications();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
