namespace mark.davison.spacetraders.desktop.ui.Services;

internal sealed class ApplicationNotificationService : IApplicationNotificationService
{
    public event EventHandler<AccountChangeArgs> AccountChanged = default!;

    public void ChangeAccount()
    {
        AccountChanged?.Invoke(this, new AccountChangeArgs());
    }

    public event EventHandler<AgentChangeArgs> AgentChanged = default!;

    public void ChangeAgent()
    {
        AgentChanged?.Invoke(this, new AgentChangeArgs());
    }
}
