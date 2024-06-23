namespace mark.davison.spacetraders.desktop.ui.State;

public abstract class StateReducer
{
    private HashSet<Type> _payloadsCanHandle;

    protected StateReducer()
    {
        _payloadsCanHandle = new();
    }

    protected void RegisterPayload<TPayload>()
    {
        _payloadsCanHandle.Add(typeof(TPayload));
    }

    public bool CanHandle(Type type)
    {
        return _payloadsCanHandle.Contains(type);
    }

    public abstract void Handle(object payload);
}

public abstract class StateReducer<TState> : StateReducer where TState : IDesktopState, new()
{
    private readonly ConcurrentDictionary<Type, List<Func<TState, object, TState>>> _callbacks;

    protected StateReducer()
    {
        _callbacks = new();
    }

    protected void RegisterResponse<TActionResponse>(Func<TState, TActionResponse, TState> callback)
        where TActionResponse : BaseActionResponse, new()
    {
        RegisterPayload<TActionResponse>();
        var actionCallbacks = _callbacks.GetOrAdd(typeof(TActionResponse), []);

        actionCallbacks.Add((state, action) =>
        {
            return callback(state, (TActionResponse)action);
        });
    }

    protected void RegisterAction<TAction>(Func<TState, TAction, TState> callback)
        where TAction : BaseAction, new()
    {
        RegisterPayload<TAction>();
        var actionCallbacks = _callbacks.GetOrAdd(typeof(TAction), []);

        actionCallbacks.Add((state, action) =>
        {
            return callback(state, (TAction)action);
        });
    }

    public override void Handle(object payload)
    {
        var state = StateStore.GetState<TState>(); // TODO: Clone???
        if (payload is Response response)
        {
            Process(state, response);
        }
        else
        {
            Process(state, payload);
        }
    }


    public void Process(TState state, object action)
    {
        if (_callbacks.TryGetValue(action.GetType(), out var callbacks))
        {
            var currentState = state;
            foreach (var callback in callbacks)
            {
                currentState = callback(currentState, action);
            }

            StateStore.SetState(currentState);
        }
    }

    public void Process(TState state, Response response)
    {
        if (_callbacks.TryGetValue(response.GetType(), out var callbacks))
        {
            if (response.Success)
            {
                var currentState = state;
                foreach (var callback in callbacks)
                {
                    currentState = callback(currentState, response);
                }

                StateStore.SetState(currentState);

                // TODO: Snackbar/global warning handle
            }
            else
            {
                // TODO: Snackbar/global error handle
            }
        }
    }
}
