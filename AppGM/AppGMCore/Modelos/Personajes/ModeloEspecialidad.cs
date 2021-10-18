using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
	/// <summary>
	/// Representa una especialidad de un <see cref="ModeloPersonaje"/>. Una especialidad es un area en la que
	/// dicho personaje tiene especial proeza 
	/// </summary>
	public class ModeloEspecialidad : ModeloBase
	{
		/// <summary>
		/// Nombre de la competencia
		/// </summary>
		[MaxLength(50)]
		public string Nombre { get; set; }

		/// <summary>
		/// Personaje que contiene esta competencia
		/// </summary>
		public virtual ModeloPersonaje PersonajeContenedor { get; set; }

		/// <summary>
		/// Tipo de esta especialidad
		/// </summary>
		private ETipoDeEspecialidad TipoDeEspecialidad { get; set; }
	}
}