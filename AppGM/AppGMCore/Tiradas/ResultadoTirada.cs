using System;

namespace AppGM.Core
{
	/// <summary>
	/// Representa el resultado de una tirada
	/// </summary>
	public readonly struct ResultadoTirada
	{
		/// <summary>
		/// Resultado de la tirada
		/// </summary>
		public readonly int resultado;

		/// <summary>
		/// Cadena que contiene los resultados de todos los dados lanzados en la tirada
		/// </summary>
		public readonly string cadena;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_resultado">Resultado de la tirada</param>
		/// <param name="_cadena">Resultados de todos los dados</param>
		public ResultadoTirada(int _resultado, string _cadena)
		{
			resultado = _resultado;
			cadena    = _cadena;
		}

		public override string ToString() => $"{cadena}{Environment.NewLine}Total: {resultado}";
	}
}
