using Avalonia.Controls;
using Avalonia.Controls.Templates;
using mark.davison.spacetraders.desktop.ui.ViewModels;
using System;

namespace mark.davison.spacetraders.desktop.ui;
public class ViewLocator : IDataTemplate
{

    public Control? Build(object? data)
    {
        if (data is null)
            return null;

        var name = data.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
        var type = Type.GetType(name);

        if (type != null)
        {
            var control = (Control)Activator.CreateInstance(type)!;
            control.DataContext = data;
            return control;
        }

        return new TextBlock { Text = "Not Found: " + name };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}
