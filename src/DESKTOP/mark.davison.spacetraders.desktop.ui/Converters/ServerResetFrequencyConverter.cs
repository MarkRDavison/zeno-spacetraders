namespace mark.davison.spacetraders.desktop.ui.Converters;

public sealed class ServerResetFrequencyConverter : MarkupExtension, IValueConverter
{
    private static ServerResetFrequencyConverter? _converter;
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string str)
        {
            return value switch
            {
                "every-three-weeks" => "Every 3 weeks",
                _ => str
            };
        }

        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    public override object ProvideValue(IServiceProvider serviceProvider) => _converter ??= new ServerResetFrequencyConverter();
}
