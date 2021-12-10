using System.Collections.Generic;

namespace AppGM.Core
{
	/// <summary>
	/// Interfaz que representa un objeto capaz de infligir daño
	/// </summary>
	public interface IInfligidorDaño
	{
		/// <summary>
		/// Delegado que representa un metodo dedicado a lidiar con eventos de infliccion de daño
		/// </summary>
		/// <param name="objetivo">Objetivo al que infligir el daño</param>
		/// <param name="argsDaño">Argumentos del daño</param>
		/// <param name="subObjetivos">Subobjetivos</param>
		public delegate void dInfligirDaño(IDañable objetivo, ModeloArgumentosDaño argsDaño, SortedList<int, SubobjetivoDaño> subObjetivos);

		/// <summary>
		/// Evento disparado cuando este objeto inflige daño a algun <see cref="IDañable"/>
		/// </summary>
		public event dInfligirDaño OnInfligioDaño;

		/// <summary>
		/// Inflige daño a un <paramref name="objetivo"/>
		/// </summary>
		/// <param name="objetivo">Objetivo al que infligir el daño</param>
		/// <param name="argsDaño">Argumentos del daño</param>
		/// <param name="subObjetivos">Subobjetivos</param>
		public void InfligirDaño(IDañable objetivo, ModeloArgumentosDaño argsDaño, SortedList<int, SubobjetivoDaño> subObjetivos = null);
	}
}
