using System.Collections.Generic;

namespace AppGM.Core
{
	/// <summary>
	/// Clase agrupa los datos que resultan de compilar una funcion
	/// </summary>
	/// <typeparam name="TipoFuncion">Tipo de la funcion generada</typeparam>
	public class ResultadoCompilacion<TipoFuncion>
	{
		/// <summary>
		/// Funcion resultante de la compilacion
		/// </summary>
		public TipoFuncion Funcion { get; set; }

		/// <summary>
		/// Bloques que componen la funcion, utilizados para posteriormente serializar la funcion
		/// </summary>
		public List<BloqueBase> Bloques { get; set; }

		/// <summary>
		/// Mensajes dejados por el compilador
		/// </summary>
		public string Mensaje { get; set; }
	}
}