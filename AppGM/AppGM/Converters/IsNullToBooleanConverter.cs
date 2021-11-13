using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AppGM
{
	/// <summary>
	/// Convierte un objeto a un valor de <see cref="Visibility"/> en base a si dicho objeto es null
	/// </summary>
	[ValueConversion(sourceType: typeof(object), targetType: typeof(Visibility), ParameterType = typeof(object))]
	public class IsNullToBooleanConverter : BaseConverter<IsNullToBooleanConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (parameter is null)
				return value is null;

			return value is not null;
		}
	}
}
