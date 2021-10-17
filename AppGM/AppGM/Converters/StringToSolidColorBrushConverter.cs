using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace AppGM
{
    /// <summary>
    /// Convierte un <see cref="string"/> con un codigo hexadecimal a un <see cref="SolidColorBrush"/>
    /// </summary>
    [ValueConversion(sourceType: typeof(string), targetType: typeof(SolidColorBrush))]
    public class StringToSolidColorBrushConverter : BaseConverter<StringToSolidColorBrushConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var colorResultante = (SolidColorBrush) new BrushConverter().ConvertFrom($"#{value}");

            if (colorResultante.CanFreeze)
                colorResultante.Freeze();

            return colorResultante;
        }
    }
}
