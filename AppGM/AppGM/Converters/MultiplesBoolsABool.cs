using System;
using System.Globalization;
using System.Windows.Data;

namespace AppGM
{
	/// <summary>
	/// Convierte varios <see cref="bool"/> en uno solo. Si el parametro es nulo y alguno de los valores en falso, se devuelve falso.
	/// Si el parametro no es nulo, ocurre lo contrario.
	/// </summary>
	[ValueConversion(sourceType: typeof(bool[]), targetType: typeof(bool), ParameterType = typeof(object))]
	public class MultiplesBoolsABool : ConvertidorDeValoresMultiples<MultiplesBoolsABool>
	{
		public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (parameter is null)
			{
				foreach (var valorActual in values)
				{
					if (valorActual is false || valorActual is not bool)
						return false;
				}
			}
			else
			{
				foreach (var valorActual in values)
				{
					if (valorActual is true || valorActual is not bool)
						return false;
				}
			}

			return true;
		}
	}
}
