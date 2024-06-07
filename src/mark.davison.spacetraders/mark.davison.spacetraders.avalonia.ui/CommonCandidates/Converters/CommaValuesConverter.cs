using Avalonia.Data.Converters;
using System.Collections;
using System.Globalization;

namespace mark.davison.spacetraders.avalonia.ui.CommonCandidates.Converters;

public sealed class CommaValuesConverter : MarkupExtension, IValueConverter
{
    public static readonly CommaValuesConverter Instance = new();
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Instance;
    }

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return null;
        }

        string? response = string.Empty;

        if (value is IEnumerable enumerable)
        {
            foreach (var e in enumerable)
            {
                if (e is string estr)
                {
                    if (!string.IsNullOrEmpty(response))
                    {
                        response += ", ";
                    }
                    response += estr;
                }
                else if (e?.ToString() is string etstr)
                {
                    if (!string.IsNullOrEmpty(response))
                    {
                        response += ", ";
                    }
                    response += etstr;
                }
            }
        }

        return response;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
