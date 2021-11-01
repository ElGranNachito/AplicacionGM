using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using AppGM.Core;
using CoolLogs;

namespace AppGM
{
	/// <summary>
	/// Convierte un arreglo de <see cref="bool"/> a un <see cref="Visibility"/>.
	/// Por cada <see cref="bool"/> que se utilice para determinar la <see cref="Visibility"/> debe haber otro, en el arreglo pasado
	/// por el parametro, que indique si el primer bool debe ser verdadero o falso
	/// </summary>
	[ValueConversion(sourceType: typeof(bool[]), targetType: typeof(Visibility), ParameterType = typeof(bool[]))]
	class MultiplesBoolsAVisibilidad : ConvertidorDeValoresMultiples<MultiplesBoolsAVisibilidad>
	{
		public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			bool[] booleanosComparacion;

			if (parameter is bool[] parametro)
			{
				booleanosComparacion = parametro;
			}
			else
			{
				SistemaPrincipal.LoggerGlobal.Log($"{nameof(parameter)} debe ser de tipo {typeof(bool[])}", ESeveridad.Error);

				return Visibility.Collapsed;
			}

			//El numero de elementos de la coleccion debe ser par por lo explicado en la descripcion de la clase
			if (values.Length != booleanosComparacion.Length)
			{
				SistemaPrincipal.LoggerGlobal.Log(@$"{nameof(booleanosComparacion.Length)} ({booleanosComparacion.Length}) no coincide con {nameof(booleanosComparacion.Length)}({booleanosComparacion.Length})!", ESeveridad.Error);

				return Visibility.Collapsed;
			}

			bool[] booleanos = new bool[values.Length];

			for (int i = 0; i < values.Length; ++i)
			{
				if (values[i] is bool b)
					booleanos[i] = b;
				else
					return Visibility.Collapsed;
			}

			for (int i = 0; i < booleanos.Length; i++)
			{
				//Si un bool no coincide con su par entonces el elemento debera estar colapsado
				if (booleanos[i] != booleanosComparacion[i])
					return Visibility.Collapsed;
			}

			//Si llegamos hasta aqui entonces todos los bools coinciden
			return Visibility.Visible;
		}
	}
}
