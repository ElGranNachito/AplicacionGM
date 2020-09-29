using System;
using System.Globalization;
using System.Windows;

namespace AppGM
{
    /// <summary>
    /// Convierte una valor booleano en un <see cref="Visibility"/> si el booleano es falso el valor de visibility sera Collapsed
    /// </summary>
    public class BooleanToVisibilityConverterColapsar : BaseConverter<BooleanToVisibilityConverterColapsar>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool valor = (bool)value;

            if (parameter == null)
                return valor ? Visibility.Visible : Visibility.Collapsed;

            return valor ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
