namespace mark.davison.spacetraders.desktop.ui.State;

public interface IState<TState> : INotifyPropertyChanged where TState : IDesktopState, new()
{
    public TState Value { get; }
}
