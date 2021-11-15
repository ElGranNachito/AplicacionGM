using System.Collections.Generic;

namespace AppGM.Core
{
	/// <summary>
	/// Modelo que representa una fuente de daño
	/// </summary>
	public partial class ModeloFuenteDeDaño : ModeloBase
	{
		/// <summary>
		/// Rol al que pertenece esta fuente de daño
		/// </summary>
		public virtual ModeloRol RolAlQuePertenece { get; set; }

		/// <summary>
		/// Nombre de esta fuente de daño
		/// </summary>
		public string NombreFuente { get; set; }

		/// <summary>
		/// Tipos de daño que inflige esta fuente
		/// </summary>
		public ETipoDeDaño TiposDeDaño { get; set; }

		/// <summary>
		/// Items que abarca esta fuente de daño
		/// </summary>
		public virtual List<ModeloDatosArma> ItemsAbarcados { get; set; } = new List<ModeloDatosArma>();

		/// <summary>
		/// Historial del daño causado por esta fuente de daño
		/// </summary>
		public virtual List<ModeloArgumentosDaño> HistorialDañoCausado { get; set; } = new List<ModeloArgumentosDaño>();
	}
}
