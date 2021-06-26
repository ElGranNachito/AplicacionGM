using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using AppGM.Core;

namespace AppGM
{
	/// <summary>
	/// Convierte un arreglo de <see cref="bool"/> a un <see cref="Visibility"/>.
	/// Por cada <see cref="bool"/> que se utilice para determinar la <see cref="Visibility"/> debe haber otro que indique
	/// si el primer <see cref="bool"/> debe ser verdadero o falso.
	/// Parametro del convertidor es la <see cref="Visibility"/> que retornar en caso de que la conversion falle. Puede
	/// dejarse en null
	/// </summary>
	[ValueConversion(sourceType: typeof(bool[]), targetType: typeof(Visibility), ParameterType = typeof(Visibility))]
	class MultiplesBoolsAVisibilidad : ConvertidorDeValoresMultiples<MultiplesBoolsAVisibilidad>
	{
		public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			bool[] booleanos = values.Cast<bool>().ToArray();

			//El numero de elementos de la coleccion debe ser par por lo explicado en la descripcion de la clase
			if (booleanos.Length % 2 != 0)
			{
				SistemaPrincipal.LoggerGlobal.Log(@$"{nameof(booleanos.Length)} no es un numero par! {nameof(booleanos)} siempre debe contener un numero par de elementos");

				if (parameter is Visibility v)
					return v;

				return Visibility.Collapsed;
			}

			for (int i = 0; i < booleanos.Length; i += 2)
			{
				//Si un bool no coincide con su par entonces el elemento debera estar colapsado
				if (booleanos[i] != booleanos[i + 1])
					return Visibility.Collapsed;
			}

			//Si llegamos hasta aqui entonces todos los bools coinciden
			return Visibility.Visible;
		}
	}
}
