using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using AppGM.Core;
using CoolLogs;

namespace AppGM
{
	/// <summary>
	/// Convierte un <see cref="bool"/> a un <see cref="SolidColorBrush"/> especificado
	/// </summary>
	[ValueConversion(sourceType: typeof(bool), targetType: typeof(SolidColorBrush), ParameterType = typeof(SolidColorBrush))]
	public class BooleanToColorConverter : BaseConverter<BooleanToColorConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool valor = (bool) value;

			SolidColorBrush colorDestino = (SolidColorBrush) parameter;

			if (colorDestino == null)
			{
				SistemaPrincipal.LoggerGlobal.Log(
					$"Error al convertir '{nameof(parameter)}' a '{nameof(SolidColorBrush)}'", ESeveridad.Error);

				return null;
			}

			if (valor)
				return colorDestino;

			return new SolidColorBrush(Color.FromRgb(255, 255, 255));
		}
	}
}
