namespace mark.davison.spacetraders.desktop.ui.State;

public abstract class StateEffects
{
    private HashSet<Type> _payloadsCanHandle;
    private readonly ConcurrentDictionary<Type, List<Func<object, IDesktopStateDispatcher, Task>>> _callbacks;
    private readonly IDesktopStateDispatcher _dispatcher;

    protected StateEffects(IDesktopStateDispatcher dispatcher)
    {
        _payloadsCanHandle = new();
        _callbacks = new();
        _dispatcher = dispatcher;
    }

    protected void RegisterPayload<TPayload>()
    {
        _payloadsCanHandle.Add(typeof(TPayload));
    }

    public bool CanHandle(Type type)
    {
        return _payloadsCanHandle.Contains(type);
    }

    protected void RegisterAction<TAction>(Func<TAction, IDesktopStateDispatcher, Task> callback)
        where TAction : BaseAction, new()
    {
        RegisterPayload<TAction>();
        var actionCallbacks = _callbacks.GetOrAdd(typeof(TAction), []);

        actionCallbacks.Add((action, dispatcher) => callback((TAction)action, _dispatcher));
    }

    // TODO: Async or not???
    public async Task Handle(object payload)
    {
        if (_callbacks.TryGetValue(payload.GetType(), out var callbacks))
        {
            foreach (var callback in callbacks)
            {
                await callback(payload, _dispatcher);
            }
        }
    }
}