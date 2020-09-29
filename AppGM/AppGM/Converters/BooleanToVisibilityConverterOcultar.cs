

using System;
using System.Globalization;
using System.Windows;

namespace AppGM
{
    /// <summary>
    /// Convierte una valor booleano en un <see cref="Visibility"/> si el booleano es falso el valor de visibility sera Hidden
    /// </summary>
    public class BooleanToVisibilityConverterOcultar : BaseConverter<BooleanToVisibilityConverterOcultar>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool valor = (bool) value;

            if (parameter == null)
                return valor ? Visibility.Visible : Visibility.Hidden;

            return valor ? Visibility.Hidden : Visibility.Visible;
        }
    }
}
