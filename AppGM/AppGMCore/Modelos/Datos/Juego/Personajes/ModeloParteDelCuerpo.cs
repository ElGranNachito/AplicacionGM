using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
	/// <summary>
	/// Representa una parte del cuerpo de un <see cref="ModeloPersonaje"/>
	/// </summary>
	public class ModeloParteDelCuerpo : ModeloBase, IModeloConSlots
	{
		/// <summary>
		/// Nombre del personaje
		/// </summary>
		[StringLength(100)]
		public string Nombre { get; set; }

		/// <summary>
		/// Multiplicador de daño que tiene esta parte del cuerpo
		/// </summary>
		public float MultiplicadorDeEstaParte { get; set; }

		/// <summary>
		/// Id del slor que contiene este modelo
		/// </summary>
		[ForeignKey(nameof(SlotContenedor))]
		public int IdSlotDueño { get; set; }

		/// <summary>
		/// Slot que contiene esta parte
		/// </summary>
		public virtual ModeloSlot SlotContenedor { get; set; }

		/// <summary>
		/// PersonajeContenedor de esta parte del cuerpo
		/// </summary>
		public virtual ModeloPersonaje PersonajeContenedor { get; set; }

		/// <summary>
		/// Slots de esta parte del cuerpo
		/// </summary>
		public virtual List<ModeloSlot> Slots { get; set; } = new List<ModeloSlot>();
	}
}
