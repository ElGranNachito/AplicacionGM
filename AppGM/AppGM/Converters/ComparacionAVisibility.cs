using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using AppGM.Core;
using CoolLogs;

namespace AppGM
{
	/// <summary>
	/// Obtiene un valor de <see cref="Visibility"/> a partir del resultado de una comparacion booleana
	/// </summary>
	[ValueConversion(sourceType: typeof(bool), targetType: typeof(Visibility))]
	public class ComparacionAVisibilityColapsar : BaseConverter<ComparacionAVisibilityColapsar>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (parameter == null)
			{
				SistemaPrincipal.LoggerGlobal.Log($"{nameof(parameter)} es null!", ESeveridad.Advertencia);
			}

			if (value == parameter)
				return Visibility.Visible;

			return Visibility.Collapsed;
		}
	}

	/// <summary>
	/// Obtiene un valor de <see cref="Visibility"/> a partir del resultado de una comparacion booleana
	/// </summary>
	[ValueConversion(sourceType: typeof(bool), targetType: typeof(Visibility))]
	public class ComparacionAVisibilityOcultar : BaseConverter<ComparacionAVisibilityOcultar>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (parameter == null)
			{
				SistemaPrincipal.LoggerGlobal.Log($"{nameof(parameter)} es null!", ESeveridad.Advertencia);
			}

			if (value == parameter)
				return Visibility.Visible;

			return Visibility.Hidden;
		}
	}
}
