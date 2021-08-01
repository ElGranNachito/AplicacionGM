using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using AppGM.Core;
using CoolLogs;
using ViewModelBloqueDeclaracionVariable = AppGM.Core.ViewModelBloqueDeclaracionVariable;

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
			//Nos aseguramos de que el valor sea en verdad un view model de un bloque
			if (value is not ViewModelBloqueFuncionBase)
			{
				SistemaPrincipal.LoggerGlobal.Log($"{nameof(value)}(Valor: {value}) no es de un {nameof(Type)} soportado!", ESeveridad.Error);

				return null;
			}

			//Si es un bloque de muestra...
			if (value is ViewModelBloqueMuestra bloqueMuestra)
				return ObtenerUserControlParaBloque(bloqueMuestra.tipoBloque, bloqueMuestra);
			
			//Si es un bloque normal...
			return ObtenerUserControlParaBloque(value.GetType(), value);
		}

		/// <summary>
		/// Obtiene el <see cref="UserControl"/> correspondiente para un <paramref name="tipoBloque"/> 
		/// </summary>
		/// <param name="tipoBloque"><see cref="Type"/> del bloque para el que se creara el <see cref="UserControl"/></param>
		/// <param name="dataContext">DataContext del <see cref="UserControl"/> que se creara</param>
		/// <returns><see cref="UserControl"/> correspondiente para el <paramref name="tipoBloque"/> pasado o null si el <see cref="tipoBloque"/>
		/// no esta soportado</returns>
		private UserControl ObtenerUserControlParaBloque(Type tipoBloque, object dataContext)
		{
			//Hacemos una increible cadena de ifs y elses porque no se puede utilizar un switch

			if (tipoBloque == typeof(ViewModelBloqueDeclaracionVariable))
			{
				return new UserControlBloqueDeclaracionVariable{DataContext = dataContext};
			}
			else if (tipoBloque == typeof(ViewModelBloqueLlamarFuncion))
			{
				return new UserControlBloqueLlamarFuncion { DataContext = dataContext };
			}
			else if (tipoBloque == typeof(ViewModelBloqueCondicionalCompleto))
			{
				return new UserControlBloqueCondicional {DataContext = dataContext};
			}

			SistemaPrincipal.LoggerGlobal.Log($"{nameof(tipoBloque)}(Valor: {tipoBloque}) no es de un {nameof(Type)} soportado!", ESeveridad.Error);

			return null;
		}
	}
}
