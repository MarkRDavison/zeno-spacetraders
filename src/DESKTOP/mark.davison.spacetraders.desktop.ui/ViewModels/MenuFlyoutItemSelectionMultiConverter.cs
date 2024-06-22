using Avalonia.Data.Converters;
using System.Collections.Generic;
using System.Globalization;

namespace mark.davison.spacetraders.desktop.ui.ViewModels;

public sealed class MenuFlyoutItemSelectionMultiConverter : IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        return values;
    }
}
