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

		IModeloConSlots ObtenerModeloDueño() => ItemDueño == null ? ParteDelCuerpoDueña : ItemDueño;

		/// <summary>
		/// Obtiene la profundidad de este slot en el inventario del <see cref="PersonajeContenedor"/>
		/// </summary>
		/// <param name="profundidadActual">Nivel actual de profundidad. Utilizado para recursion</param>
		/// <returns>Profundidad de este slot</returns>
		public int ObtenerProfundidad(int profundidadActual = 0)
		{
			var dueñoActual = ObtenerModeloDueño();

			if (dueñoActual != null)
				return dueñoActual.ObtenerProfundidad(++profundidadActual);

			return profundidadActual;
		}
	}
}
