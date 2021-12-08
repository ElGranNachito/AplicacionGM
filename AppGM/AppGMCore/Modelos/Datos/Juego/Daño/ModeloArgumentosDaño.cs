using System.Collections.Generic;

namespace AppGM.Core
{
	/// <summary>
	/// Argumentos de un daño infligido a un <see cref="IDañable"/>
	/// </summary>
	public partial class ModeloArgumentosDaño : ModeloBase
	{
		/// <summary>
		/// Daño total
		/// </summary>
		public int DañoTotal { get; set; }

		/// <summary>
		/// Daño final aplicado al objetivo luego de atravesar todas las reducciones
		/// </summary>
		public int DañoFinal { get; set; }

		/// <summary>
		/// Rango del daño
		/// </summary>
		public ERango? Rango { get; set; }

		/// <summary>
		/// Nivel de la magia
		/// </summary>
		public ENivelMagia? NivelMagia { get; set; }

		/// <summary>
		/// Tipo del daño
		/// </summary>
		public ETipoDeDaño? TipoDeDaño { get; set; }

		/// <summary>
		/// Argumentos de la tirada en caso de que este daño se haya infligido a traves de una tirada
		/// </summary>
		public virtual ModeloHistorialTirada Tirada { get; set; }

		/// <summary>
		/// Infligidores del daño
		/// </summary>
		public virtual List<ModeloInfligidorDaño> InfligidoresDaño { get; set; } = new List<ModeloInfligidorDaño>();

		/// <summary>
		/// Objetivos del daño
		/// </summary>
		public virtual List<ModeloDañable> Objetivos { get; set; } = new List<ModeloDañable>();

		/// <summary>
		/// Fuentes de daño abarcadas
		/// </summary>
		public virtual List<ModeloFuenteDeDaño> FuentesDeDañoAbarcadas { get; set; } = new List<ModeloFuenteDeDaño>();
	}
}