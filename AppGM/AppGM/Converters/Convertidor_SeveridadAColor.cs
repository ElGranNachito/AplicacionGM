using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using AppGM.Core;
using CoolLogs;

namespace AppGM
{
	/// <summary>
	/// Convierte de un valor de <see cref="ESeveridad"/> a un <see cref="SolidColorBrush"/>
	/// </summary>
	[ValueConversion(sourceType: typeof(ESeveridad), targetType: typeof(SolidColorBrush))]
	public class Convertidor_SeveridadAColor : BaseConverter<Convertidor_SeveridadAColor>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			SolidColorBrush colorResultante = null;

			if (value is ESeveridad s)
			{
				switch (s)
				{
					case ESeveridad.Debug:
					case ESeveridad.Info:
						colorResultante = new SolidColorBrush(Color.FromRgb(114, 201, 76));
						break;
					case ESeveridad.Advertencia:
						colorResultante = new SolidColorBrush(Color.FromRgb(255, 255, 0));
						break;
					case ESeveridad.Error:
						colorResultante = new SolidColorBrush(Color.FromRgb(255, 0, 0));
						break;
					default:
					{
						SistemaPrincipal.LoggerGlobal.Log($"Valor de {nameof(ESeveridad)} no soportado! {s}", ESeveridad.Error);

						colorResultante = new SolidColorBrush(Color.FromRgb(255, 255, 255));

						break;
					}
				}
			}

			if(colorResultante is null)
				SistemaPrincipal.LoggerGlobal.Log($"{nameof(value)} fue de un tipo incorrecto! {value.GetType()}");

			colorResultante ??= new SolidColorBrush(Color.FromRgb(255, 255, 255));

			if (colorResultante.CanFreeze)
				colorResultante.Freeze();

			return colorResultante;
		}
	}
}
