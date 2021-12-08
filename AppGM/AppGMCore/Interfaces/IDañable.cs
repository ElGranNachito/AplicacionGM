using System.Collections.Generic;

namespace AppGM.Core
{
	/// <summary>
	/// Representa una entidad que puede ser dañada
	/// </summary>
	public interface IDañable
	{
		/// <summary>
		/// Delegado que representa metodos destinado a lidiar con eventos de recepcion de daño
		/// </summary>
		/// <param name="argsDaño"></param>
		/// <param name="subObjetivos"></param>
		public delegate void dDañado(ModeloArgumentosDaño argsDaño, SortedList<int, SubobjetivoDaño> subObjetivos = null);

		/// <summary>
		/// Evento disparado cuando este objeto recibe daño
		/// </summary>
		public event dDañado OnDañado;

		/// <summary>
		/// Inflige daño a este objeto
		/// </summary>
		/// <param name="argsDaño">Argumentos del daño</param>
		/// <param name="subObjetivos">Subobjetivos</param>
		void Dañar(ModeloArgumentosDaño argsDaño, SortedList<int, SubobjetivoDaño> subObjetivos = null);
	}
}