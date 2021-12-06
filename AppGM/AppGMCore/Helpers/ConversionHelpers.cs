using System;
using System.Globalization;

namespace AppGM.Core
{
	/// <summary>
	/// Clase que contiene metodos destinados a asistir en la conversion entre tipos
	/// </summary>
	public static class ConversionHelpers
	{
		/// <summary>
		/// Intenta parsear una <see cref="cadena"/> a un <see cref="int"/>
		/// </summary>
		/// <param name="cadena">Cadena que parsear</param>
		/// <param name="valorPorDefecto">Valor que devolver en caso de que no se pueda parsear la <param name="cadena"></param></param>
		/// <returns><see cref="int"/> parseado o el <paramref name="valorPorDefecto"/> en caso de que no se pueda parsear</returns>
		public static int ParseToIntIfValid(this string cadena, int valorPorDefecto = 0)
		{
			if (!int.TryParse(cadena, out var resultado))
				return valorPorDefecto;

			return resultado;
		}

		/// <summary>
		/// Intenta parsear una <see cref="cadena"/> a un <see cref="decimal"/>
		/// </summary>
		/// <param name="cadena">Cadena que parsear</param>
		/// <param name="valorPorDefecto">Valor que devolver en caso de que no se pueda parsear la <param name="cadena"></param></param>
		/// <param name="precision">Precision que debe tener el <see cref="decimal"/> devuelto</param>
		/// <param name="escala">Escala que debe tener el <see cref="decimal"/> devuelto</param>
		/// <returns><see cref="decimal"/> parseado o el <paramref name="valorPorDefecto"/> en caso de que no se pueda parsear</returns>
		public static decimal ParseToDecimalIfValid(this string cadena, decimal valorPorDefecto = decimal.MinValue, int precision = 10, int escala = 5)
		{
			if (string.IsNullOrWhiteSpace(cadena))
				return valorPorDefecto;

			//Separamos el numero en parte entera y decimal
			var secciones = cadena.Split(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

			//Obtenemos la parte entera del numero y limitamos su longitud de acuerdo a la precision
			var parteEntera = secciones[0].Substring(0, Math.Min(precision - escala, secciones[0].Length));

			string parteDecimal = string.Empty;

			//Si tiene parte decimal, la obtenemos tambien y limitamos su longitud de acuerdo a la precision
			if (secciones.Length > 1)
				parteDecimal = secciones[1].Substring(0, Math.Min(escala, secciones[1].Length));

			if (decimal.TryParse(
				$"{parteEntera}{CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator}{parteDecimal}",
				NumberStyles.AllowDecimalPoint,
				null,
				out var resultado))
			{
				return resultado;
			}

			return valorPorDefecto;
		}
	}
}