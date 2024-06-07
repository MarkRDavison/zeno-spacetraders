namespace mark.davison.spacetraders.avalonia.ui.CommonCandidates;

public abstract partial class AsyncViewModel : PageViewModel
{
    public async void Init()
    {
        Loading = true;

        await InitializeAsync();

        Loading = false;
    }

    protected virtual async Task InitializeAsync()
    {
        await Task.CompletedTask;
    }

    [ObservableProperty]
    private bool _loading;
}
