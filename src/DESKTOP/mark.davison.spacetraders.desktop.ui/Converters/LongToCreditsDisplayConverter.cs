namespace mark.davison.spacetraders.desktop.ui.Converters;

public sealed class LongToCreditsDisplayConverter : MarkupExtension, IValueConverter
{
    private static LongToCreditsDisplayConverter? _converter;
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is long credits)
        {
            return "₡" + credits.ToString("N0");
        }

        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        _converter ??= new LongToCreditsDisplayConverter();

        return _converter;
    }
}
