using System;
using System.Globalization;
using System.Windows.Data;

namespace AppGM
{
	/// <summary>
	/// Devuelve un valor de opacidad en base a si un objeto es nulo o no
	/// </summary>
	[ValueConversion(typeof(object), typeof(float))]
	public class IsNullToOpacityConverter : BaseConverter<IsNullToOpacityConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return BooleanToOpacityConverter.instanciaConverter.Value.Convert(value is null, targetType, parameter, culture);
		}
	}
}
