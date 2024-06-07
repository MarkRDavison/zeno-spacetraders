namespace mark.davison.spacetraders.avalonia.ui;

public class ViewLocator : IDataTemplate
{

    public Control? Build(object? data)
    {
        if (data is null)
        {
            return null;
        }

        // TODO: Manauly register
        var name = data.GetType().Name!.Replace("ViewModel", "View", StringComparison.Ordinal);

        var type = Type.GetType("mark.davison.spacetraders.avalonia.ui." + name);

        if (type != null)
        {
            var control = (Control)Activator.CreateInstance(type)!;
            control.DataContext = data;

            if (data is AsyncViewModel asyncData)
            {
                asyncData.Init();
            }

            return control;
        }

        return new TextBlock { Text = "Not Found: " + name };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}
