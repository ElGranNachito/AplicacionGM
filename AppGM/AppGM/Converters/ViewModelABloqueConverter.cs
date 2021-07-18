using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using AppGM.Core;
using CoolLogs;

namespace AppGM
{
	/// <summary>
	/// Convierte de un <see cref="ViewModelBloqueFuncionBase"/> a un <see cref="UserControl"/>.
	/// Parametro 
	/// </summary>
	[ValueConversion(sourceType: typeof(ViewModelBloqueFuncionBase), targetType: typeof(UserControl), ParameterType = typeof(bool))]
	public class ViewModelABloqueConverter : BaseConverter<ViewModelABloqueConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is not ViewModelBloqueFuncionBase && value is not Type)
			{
				SistemaPrincipal.LoggerGlobal.Log($"{nameof(value)} no es de un {nameof(Type)} soportado!", ESeveridad.Error);

				return null;
			}

			switch (value)
			{
				case ViewModelBloqueDeclaracionVariable:
					return new UserControlBloqueDeclaracionVariable {DataContext =value};
				case ViewModelBloqueLlamarFuncion:
					return new UserControlBloqueLlamarFuncion {DataContext = value};
				case ViewModelBloqueCondicionalCompleto:
					return new UserControlBloqueCondicional {DataContext = value};
				default:
					return null;
			}
		}
	}
}
