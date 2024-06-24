namespace mark.davison.spacetraders.desktop.ui.State;

public class DesktopStateDispatcher : IDesktopStateDispatcher
{
    private readonly IDesktopActionSubscriber _actionSubscriber;
    private readonly IServiceProvider _services;
    private bool _initialized;

    public DesktopStateDispatcher(
        IDesktopActionSubscriber actionSubscriber,
        IServiceProvider services)
    {
        _actionSubscriber = actionSubscriber;
        _services = services;
    }

    public async void Dispatch(object payload)
    {
        StateStore.ApplyReducers(_services, payload);
        _actionSubscriber.Notify(payload);
        await StateStore.DispatchEffects(_services, payload);
    }
}
