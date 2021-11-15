using System;
using System.Globalization;
using System.Windows.Data;
using AppGM.Core;
using CoolLogs;

namespace AppGM
{
	/// <summary>
	/// Invierte el valor de un <see cref="bool"/>
	/// </summary>
	[ValueConversion(typeof(bool), typeof(bool))]
	public class NegarBoolConverter : BaseConverter<NegarBoolConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool b)
				return !b;

			SistemaPrincipal.LoggerGlobal.Log($"{nameof(value)} debe ser de tipo booleano", ESeveridad.Error);

			return false;
		}
	}
}
