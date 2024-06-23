namespace mark.davison.spacetraders.desktop.ui.State;

public static class StateStore
{
    private static readonly ConcurrentDictionary<Type, List<Action>> _stateChangeCallbacks;
    private static readonly ConcurrentDictionary<Type, object> _state;

    static StateStore()
    {
        _stateChangeCallbacks = new();
        _state = new();
    }

    public static void RegisterStateChangeCallback<TState>(Action callback) where TState : IDesktopState, new()
    {
        var callbacks = _stateChangeCallbacks.GetOrAdd(typeof(TState), []);
        callbacks.Add(callback);
    }

    public static TState GetState<TState>() where TState : IDesktopState, new()
    {
        return (TState)_state.GetOrAdd(typeof(TState), _ => new TState());
    }

    public static void SetState<TState>(TState state) where TState : IDesktopState, new()
    {
        _state[typeof(TState)] = state;

        if (_stateChangeCallbacks.TryGetValue(typeof(TState), out var callbacks))
        {
            Dispatcher.UIThread.Invoke(() =>
            {
                foreach (var callback in callbacks)
                {
                    callback();
                }
            });
        }
    }
}
