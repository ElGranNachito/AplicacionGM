using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace AppGM
{

	/// <summary>
	/// Convierte de un <see cref="bool"/> a un <see cref="SolidColorBrush"/>
	/// </summary>
	[ValueConversion(sourceType: typeof(bool), targetType: typeof(SolidColorBrush))]
	public class BoolAColor : ConvertidorDeValoresMultiples<BoolAColor>
	{
		public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values.Length < 3)
				return new SolidColorBrush(Color.FromRgb(0, 0, 0));

			if(values[0] is bool b &&
			   values[1] is SolidColorBrush color1 &&
			   values[2] is SolidColorBrush color2)
			{
				return b ? color1 : color2;
			}

			return new SolidColorBrush(Color.FromRgb(0, 0, 0));
		}
	}
}
