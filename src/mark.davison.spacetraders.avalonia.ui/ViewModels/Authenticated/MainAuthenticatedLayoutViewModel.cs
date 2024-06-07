namespace mark.davison.spacetraders.avalonia.ui.ViewModels.Authenticated;

public partial class MainAuthenticatedLayoutViewModel : AsyncViewModel
{

    public MainAuthenticatedLayoutViewModel(IServiceProvider serviceProvider)
    {
        Pages.Add(serviceProvider.GetRequiredService<AgentInfoPageViewModel>());
        Pages.Add(serviceProvider.GetRequiredService<ShipsInfoPageViewModel>());
        Pages.Add(serviceProvider.GetRequiredService<WaypointsInfoPageViewModel>());

        PageNames.Add("Agent");
        PageNames.Add("Ships");
        PageNames.Add("Waypoints");

        ActiveMainAuthPageViewModel = Pages[SelectedIndex];
    }
    partial void OnSelectedIndexChanged(int oldValue, int newValue)
    {
        if (oldValue != newValue)
        {
            ActiveMainAuthPageViewModel = Pages[newValue];
            ActiveMainAuthPageViewModel.Init();
        }
    }

    [ObservableProperty]
    private AsyncViewModel _activeMainAuthPageViewModel;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ActiveMainAuthPageViewModel))]
    private int _selectedIndex;

    public List<AsyncViewModel> Pages { get; } = [];
    public List<string> PageNames { get; } = [];

}
