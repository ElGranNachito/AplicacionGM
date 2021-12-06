using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;
using AppGM.Core;
using CoolLogs;

namespace AppGM
{
	/// <summary>
	/// Convertidor destinado a acceder a un elemento de un arreglo por indice
	/// </summary>
	[ValueConversion(typeof(IList), typeof(object), ParameterType = typeof(int))]
	public class AccederAColeccionPorIndiceConverter : BaseConverter<AccederAColeccionPorIndiceConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (parameter is null)
				return null;

			if (value is IList coleccion && int.TryParse(parameter.ToString(), out int indice))
			{
				if (indice < 0 || indice >= coleccion.Count)
				{
					SistemaPrincipal.LoggerGlobal.Log($"{nameof(indice)} no es valido", ESeveridad.Error);

					return null;
				}
				
				return coleccion[indice];
			}

			SistemaPrincipal.LoggerGlobal.Log($"{nameof(value)}({value}) o {nameof(parameter)}({parameter}) no son validos", ESeveridad.Error);

			return null;
		}
	}
}
