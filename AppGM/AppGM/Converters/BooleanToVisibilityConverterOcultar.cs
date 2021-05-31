using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AppGM
{
	/// <summary>
	/// Convierte una valor booleano en un <see cref="Visibility"/> si el booleano es falso el valor de visibility sera Hidden.
	/// Si el parametro de <see cref="Convert"/> no es null los valores de <see cref="Visibility"/> a los que corresponden los
	/// valores de <see cref="bool"/> se invierten.
	/// </summary>
	[ValueConversion(sourceType: typeof(bool), targetType: typeof(Visibility))]
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
