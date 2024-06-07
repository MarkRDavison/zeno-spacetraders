namespace mark.davison.spacetraders.avalonia.ui;

public partial class App : Application
{
    private IServiceProvider _services = default!;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Startup += OnStartup;
            desktop.Exit += OnExit;

            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            BindingPlugins.DataValidators.RemoveAt(0);

            var collection = new ServiceCollection();

            collection.AddSpacetradersUi();

            _services = collection.BuildServiceProvider();

            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(_services)
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private async void OnStartup(object? s, ControlledApplicationLifetimeStartupEventArgs e)
    {
        var persistentState = _services.GetRequiredService<IPersistentState>();

        await persistentState.LoadAccountsAsync(CancellationToken.None);
    }

    private void OnExit(object? s, ControlledApplicationLifetimeExitEventArgs e)
    {
    }
}