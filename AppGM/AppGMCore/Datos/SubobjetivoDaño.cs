namespace AppGM.Core
{
	/// <summary>
	/// Representa en un receptor de daño
	/// </summary>
	public record SubobjetivoDaño
	{
		/// <summary>
		/// Dañable
		/// </summary>
		public IDañable dañable;

		/// <summary>
		/// Indica si dañar al contenido de este subobjetivo
		/// </summary>
		public bool dañarContenido;

		/// <summary>
		/// Indica la profundidad maxima a la que esparcir el daño
		/// </summary>
		public int profundidadMaxima;
	}
}
