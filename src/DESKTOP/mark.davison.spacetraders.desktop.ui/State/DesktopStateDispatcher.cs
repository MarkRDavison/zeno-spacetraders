namespace mark.davison.spacetraders.desktop.ui.State;

public class DesktopStateDispatcher : IDesktopStateDispatcher
{
    private readonly IDesktopActionSubscriber _actionSubscriber;
    private readonly IServiceProvider _services;
    private readonly List<StateReducer> _stateReducers = [];
    private readonly List<StateEffects> _stateEffects = [];
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
        if (!_initialized)
        {
            _stateReducers.AddRange(_services.GetRequiredService<IEnumerable<StateReducer>>());
            _stateEffects.AddRange(_services.GetRequiredService<IEnumerable<StateEffects>>());
            _initialized = true;
        }

        var type = payload.GetType();
        foreach (var stateReducer in _stateReducers)
        {
            if (stateReducer.CanHandle(type))
            {
                stateReducer.Handle(payload);
            }
        }
        _actionSubscriber.Notify(payload);
        foreach (var stateEffects in _stateEffects)
        {
            if (stateEffects.CanHandle(type))
            {
                await stateEffects.Handle(payload);
            }
        }
    }
}
