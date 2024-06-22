namespace mark.davison.spacetraders.desktop.common;

public class ViewLocator : IDataTemplate
{
    private static readonly Dictionary<Type, Func<Control>> _creation = new();

    public static void Register<TViewModel, TView>()
        where TViewModel : ObservableObject
        where TView : Control, new()
    {
        _creation.Add(typeof(TViewModel), () => new TView());
    }

    public Control? Build(object? data)
    {
        if (data is not null && _creation.TryGetValue(data.GetType(), out var creator))
        {
            var control = creator();
            control.DataContext = data;
            return control;
        }

        return new TextBlock { Text = "Not Found: " + data?.GetType().Name };
    }

    public bool Match(object? data)
    {
        return data is not null && _creation.ContainsKey(data.GetType());
    }
}
