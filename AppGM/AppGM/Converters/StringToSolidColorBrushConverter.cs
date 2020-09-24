using System;
using System.Globalization;
using System.Windows.Media;

namespace AppGM
{
    public class StringToSolidColorBrushConverter : BaseConverter<StringToSolidColorBrushConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (SolidColorBrush) new BrushConverter().ConvertFrom($"#{value}");
        }
    }
}
