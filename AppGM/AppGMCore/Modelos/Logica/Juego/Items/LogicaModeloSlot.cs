using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AppGM.Core
{
	/// <summary>
	/// Clase que contiene la logica para <see cref="ModeloSlot"/>
	/// </summary>
	public partial class ModeloSlot
	{
		/// <summary>
		/// Contiene el valor de <see cref="PersonajeContenedor"/>
		/// </summary>
		private ModeloPersonaje mPersonajeContenedor;

		/// <summary>
		/// Espacio disponible en el slot
		/// </summary>
		[NotMapped]
		public decimal EspacioDisponible => EspacioTotal - EspacioOcupado;

		/// <summary>
		/// Espacio ocupado del slot
		/// </summary>
		[NotMapped]
		public decimal EspacioOcupado => ItemsAlmacenados.Sum(i => i.EspacioQueOcupa);
	}
}
