namespace mark.davison.spacetraders.avalonia.ui.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, IDisposable
{
    private bool disposedValue;
    private readonly IApplicationNotificationService _applicationNotificationService;

    public MainWindowViewModel() : this(DependencyInjectionExtensions.DesignTimeServiceProvider)
    {

    }

    public MainWindowViewModel(IServiceProvider serviceProvider)
    {
        Pages.Add(serviceProvider.GetRequiredService<LandingPageViewModel>());
        Pages.Add(serviceProvider.GetRequiredService<MainAuthenticatedLayoutViewModel>());

        ActivePageViewModel = Pages[SelectedIndex];

        _applicationNotificationService = serviceProvider.GetRequiredService<IApplicationNotificationService>();

        SetupNotifications();
    }

    private void SetupNotifications()
    {
        _applicationNotificationService.UserChanged += OnUserChanged;
    }

    private void TeardownNotifications()
    {
        _applicationNotificationService.UserChanged -= OnUserChanged;
    }

    private void OnUserChanged(object? sender, PersistedAccount e)
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            SelectedIndex = Pages.FindIndex(_ => _.GetType() == typeof(MainAuthenticatedLayoutViewModel));
        });
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ActivePageViewModel))]
    private int _selectedIndex;

    partial void OnSelectedIndexChanged(int oldValue, int newValue)
    {
        ActivePageViewModel = Pages[newValue];
    }

    [ObservableProperty]
    private PageViewModel _activePageViewModel;

    public List<PageViewModel> Pages { get; } = [];

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
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
