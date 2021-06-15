
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using AppGM.Core;
using CoolLogs;

namespace AppGM
{
	/// <summary>
	/// Selector de <see cref="DataTemplate"/> para un <see cref="ItemsControl"/> que contiene una lista de
	/// <see cref="ViewModelBloqueFuncionBase"/>
	/// </summary>
	public class BloqueDataTemplateSelector : DataTemplateSelector
	{
		public bool EsBloqueDeMuestra { get; set; } = false;

		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			//Si el vm es null...
			if (item == null)
			{
				//Logueamos el error
				SistemaPrincipal.LoggerGlobal.Log($"{nameof(item)} fue null. {nameof(container)}: {container}", ESeveridad.Error);

				//Devolvemos null
				return null;
			}

			DataTemplate resultado = new DataTemplate();

			if (EsBloqueDeMuestra)
			{
				resultado.DataType = typeof(UserControlBloqueMuestra);
				resultado.VisualTree = new FrameworkElementFactory(typeof(UserControlBloqueMuestra));

				return resultado;
			}

			//Revisamos el tipo del vm
			switch (item)
			{
				case ViewModelBloqueDeclaracionVariable:
					resultado.DataType = typeof(UserControlBloqueDeclaracionVariable);
					resultado.VisualTree = new FrameworkElementFactory(typeof(UserControlBloqueDeclaracionVariable));
					break;
				case ViewModelBloqueLlamarFuncion:
					resultado.DataType = typeof(UserControlBloqueLlamarFuncion);
					resultado.VisualTree = new FrameworkElementFactory(typeof(UserControlBloqueLlamarFuncion));
					break;
				default:
					//Si el tipo del vm no esta abarcado
					SistemaPrincipal.LoggerGlobal.Log($"{nameof(Type)} de {nameof(item)} no fue {nameof(ViewModelBloqueFuncionBase)}", ESeveridad.Error);
					break;
			}

			//Devolvemos el resultado
			return resultado;
		}
	}
}