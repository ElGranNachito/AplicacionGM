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
			ViewModelBloqueFuncionBase vmBloque = (ViewModelBloqueFuncionBase) value;

			if (vmBloque == null)
			{
				SistemaPrincipal.LoggerGlobal.Log($"{nameof(value)} no es de un {nameof(Type)} soportado!", ESeveridad.Error);

				return null;
			}

			switch (value)
			{
				case ViewModelBloqueDeclaracionVariable vm:
					return new UserControlBloqueDeclaracionVariable {DataContext = vm};
				case ViewModelBloqueLlamarFuncion vm:
					return new UserControlBloqueLlamarFuncion {DataContext = vm};
				case ViewModelBloqueCondicionalCompleto vm:
					return new UserControlBloqueCondicional {DataContext = vm};
				default:
					return null;
			}
		}
	}
}
