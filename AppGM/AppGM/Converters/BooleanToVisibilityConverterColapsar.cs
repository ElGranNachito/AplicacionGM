using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace AppGM
{
    /// <summary>
    /// Convierte una valor booleano en un <see cref="Visibility"/> si el booleano es falso el valor de visibility sera Collapsed.
    /// Si el parametro de <see cref="Convert"/> no es null los valores de <see cref="Visibility"/> a los que corresponden los
    /// valores de <see cref="bool"/> se invierten.
    /// </summary>
    [ValueConversion(sourceType: typeof(bool), targetType: typeof(Visibility))]
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

	/// <summary>
	/// <para>
	///		Convierte varios valores <see cref="bool"/> a un valor de <see cref="Visibility"/>
	/// </para>
	/// <para>
	///		Si el parametro pasado no es null, el valor que deben tener todos los booleanos para que el resultado sea
	///		<see cref="Visibility.Visible"/>, pasa a ser true. Es decir que si el parametro no es null, el valor de
	///		visibilidad devuelto sera collapsed cuando cualquiera de los booleanos es verdadero
	/// </para>
	/// </summary>
	[ValueConversion(sourceType: typeof(bool[]), targetType: typeof(Visibility), ParameterType = typeof(object))]
	public class BooleanToVisibilityConverterAllTrueColapsarMultiple : ConvertidorDeValoresMultiples<BooleanToVisibilityConverterAllTrueColapsarMultiple>
	{
		public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (parameter == null)
			{
				foreach (var valor in values)
				{
					//Si alguno de los valores es falso estonces devolvemos collapsed
					if (valor is false || valor is not bool)
					{
						return Visibility.Collapsed;
					}
				}
            }
			else
			{
				foreach (var valor in values)
				{
					//Si alguno de los valores es falso estonces devolvemos collapsed
					if (valor is true || valor is not bool)
					{
						return Visibility.Collapsed;
					}
				}
			}

			//Si llegamos hasta aqui es porque todos los valores son verdaderos
			return Visibility.Visible;
		}
	}
}
