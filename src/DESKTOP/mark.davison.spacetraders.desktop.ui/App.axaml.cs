namespace mark.davison.spacetraders.desktop.ui;

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

            BindingPlugins.DataValidators.RemoveAt(0);

            ViewLocatorRegistration.RegisterViews();

            _services = new ServiceCollection()
                .AddSpacetradersDesktop()
                .AddSpacetradersCommon()
                .BuildServiceProvider();

            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(_services),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void OnStartup(object? s, ControlledApplicationLifetimeStartupEventArgs e)
    {

    }

    private void OnExit(object? s, ControlledApplicationLifetimeExitEventArgs e)
    {
        var authService = _services.GetRequiredService<IDesktopAuthenticationService>();

        authService.PersistLogin();
    }
}