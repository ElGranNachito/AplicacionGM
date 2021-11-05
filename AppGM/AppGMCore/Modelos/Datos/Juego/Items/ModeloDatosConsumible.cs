using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
	/// <summary>
	/// Modelo que contiene los datos de un consumible
	/// </summary>
	public class ModeloDatosConsumible : ModeloBase
	{
		/// <summary>
		/// Usos totales de este consumible
		/// </summary>
		public int UsosTotales { get; set; }

		/// <summary>
		/// Usos restantes de este consumible
		/// </summary>
		public int UsosRestantes { get; set; }

		/// <summary>
		/// Clave foranea del <see cref="Item"/>
		/// </summary>
		[ForeignKey(nameof(Item))]
		public int IDItem { get; set; }

		/// <summary>
		/// Item al que pertenece
		/// </summary>
		public virtual ModeloItem Item { get; set; }
	}
}