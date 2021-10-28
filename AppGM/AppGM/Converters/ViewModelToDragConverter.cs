using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Linq;

using AppGM.Core;

using CoolLogs;

namespace AppGM
{
	/// <summary>
	/// Convierte un <see cref="ViewModel"/> a un <see cref="UserControl"/>.
	/// Este convertidor toma como parametro la ventana en la que se realiza la conversion para determinar si es la ventana
	/// actualmente siendo utilizada. Esto sirve para no crear un drag en dos ventana a la vez.
	/// </summary>
	[ValueConversion(sourceType: typeof(ViewModel), targetType: typeof(UserControl), ParameterType = typeof(Window))]
	public class ViewModelToDragConverter : BaseConverter<ViewModelToDragConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			//Como este es un convertidor para contenido de drags nos tenemos que asegurar de que se este usando en el contexto apropiado
			if (!SistemaPrincipal.Drag.HayUnDragActivo)
				return null;

			//Si es un drag con un unico elemento entonces queremos pasar el valor como un unico elemento en lugar de una lista
			if (SistemaPrincipal.Drag.TipoDragActivo == ETipoDrag.Unico)
				value = SistemaPrincipal.Drag.DatosDrag[0];
			else
				value = SistemaPrincipal.Drag.DatosDrag;

			//Revisamos que la ventana sea la actualmente seleccionada
			switch (value)
			{
				case ViewModelCarta vmCarta:
					return new UserControlCarta
					{
						DataContext = vmCarta,
						MaxWidth    = 100,
						MaxHeight   = 125,
						Width = 100,
						Height = 125
					};

				case ViewModelBloqueFuncionBase vm:
					return ViewModelABloqueConverter.instanciaConverter.Value.Convert(vm, typeof(UserControl), null, CultureInfo.CurrentCulture);

				case List<IDrageable> listaElementos:
				{
					if(listaElementos.Cast<ViewModelElementoArbolItemInventario>() is {} vmElementoInventario)
						return new UserControlDragElementoInventario { DataContext = new ViewModelDragElementoInventario(vmElementoInventario.ToList()) };

					return null;
				}
					
				default:
					return null;
			}
		}
	}
}
