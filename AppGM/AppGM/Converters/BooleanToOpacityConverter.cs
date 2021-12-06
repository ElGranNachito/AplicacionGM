using System;
using System.Globalization;
using System.Windows.Data;

namespace AppGM
{
	/// <summary>
	/// Convierte un valor <see cref="bool"/> a un valor de opacidad
	/// </summary>
	[ValueConversion(typeof(bool), typeof(float))]
	public class BooleanToOpacityConverter : BaseConverter<BooleanToOpacityConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool b)
			{
				if (parameter is null)
					return b ? 1 : 0;

				return b ? 0 : 1;
			}

			return 0;
		}
	}
}
