using System;

namespace AppGM.Core
{
	/// <summary>
	/// Representa el resultado de una tirada
	/// </summary>
	[AccesibleEnGuraScratch]
	public readonly struct ResultadoTirada
	{
		/// <summary>
		/// Resultado de la tirada
		/// </summary>
		[AccesibleEnGuraScratch("Resultado")]
		public readonly int resultado;

		/// <summary>
		/// Cadena que contiene los resultados de todos los dados lanzados en la tirada
		/// </summary>
		public readonly string resultadoDetallado;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_resultado">Resultado de la tirada</param>
		/// <param name="_resultadoDetallado">Resultados de todos los dados</param>
		public ResultadoTirada(int _resultado, string _resultadoDetallado)
		{
			resultado = _resultado;
			resultadoDetallado    = _resultadoDetallado;
		}

		public override string ToString() => resultadoDetallado;

		public string ToString(char modo)
		{
			switch (modo)
			{
				case 'c':
					return $"{resultadoDetallado}{Environment.NewLine}Total: {resultado}";

				default:
					return resultadoDetallado;
			}
		}
	}
}
