using System.Linq;

namespace AppGM.Core
{
	/// <summary>
	/// Clase que contiene la logica para <see cref="ModeloSlot"/>
	/// </summary>
	public partial class ModeloSlot
	{
		/// <summary>
		/// Espacio disponible en el slot
		/// </summary>
		public decimal EspacioDisponible => ItemsAlmacenados.Sum(i => i.EspacioQueOcupa);
	}
}
