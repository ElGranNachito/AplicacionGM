using System.Text.RegularExpressions;

namespace AppGM.Core
{
	/// <summary>
	/// Clase que contiene la logica de <see cref="TIFuncionHandlerEvento{TOtro}"/>
	/// </summary>
	/// <typeparam name="TOtro"></typeparam>
	public partial class TIFuncionHandlerEvento<TOtro>

		where TOtro: ModeloBase
	{
		/// <summary>
		/// Indica si esta funcion se encuentra vinculada aun evento con el <paramref name="nombreEvento"/> indicado
		/// </summary>
		/// <param name="nombreEvento">Nombre del evento</param>
		/// <returns><see cref="bool"/> indicando si hay un evento vinculado con el <paramref name="nombreEvento"/> especificado</returns>
		public bool EstaVinculadoA(string nombreEvento) => Regex.IsMatch(NombresEventosVinculados, nombreEvento);

		/// <summary>
		/// Añade un <paramref name="nombreEvento"/> a <see cref="NombresEventosVinculados"/>
		/// </summary>
		/// <param name="nombreEvento">Nombre del evento que añadir</param>
		public void VincularA(string nombreEvento) => NombresEventosVinculados += NombresEventosVinculados.Length == 0 ? nombreEvento : $";{nombreEvento}";

		/// <summary>
		/// Quita un <paramref name="nombreEvento"/> de <see cref="NombresEventosVinculados"/>
		/// </summary>
		/// <param name="nombreEvento">Nombre del evento que quitar</param>
		public void DesvincularDe(string nombreEvento) => Regex.Replace(NombresEventosVinculados, $";?{nombreEvento}(?(;)(?(?<!;{nombreEvento});|)|)", "");
	}
}
