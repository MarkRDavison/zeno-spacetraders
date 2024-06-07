namespace mark.davison.spacetraders.avalonia.ui.Services;

public sealed class ApplicationNotificationService : IApplicationNotificationService
{
    public event EventHandler<PersistedAccount> UserChanged = default!;

    public void OnUserChanged(PersistedAccount user)
    {
        UserChanged?.Invoke(this, user);
    }

    public event EventHandler<NavigationRequest> NavigationRequest = default!;

    public void OnNavigationRequest(NavigationRequest request)
    {
        NavigationRequest?.Invoke(this, request);
    }
}
