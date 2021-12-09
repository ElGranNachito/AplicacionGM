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
		public IDañable objetivo;

		/// <summary>
		/// Indica si dañar al contenido de este subobjetivo
		/// </summary>
		public bool dañarContenido;

		/// <summary>
		/// Indica la profundidad maxima a la que esparcir el daño
		/// </summary>
		public int profundidadMaxima;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_objetivo"><see cref="IDañable"/> que recibira el daño</param>
		/// <param name="_dañarContenido">Indica si tambien se debe dañar el contenido de este <see cref="IDañable"/></param>
		/// <param name="_profundidadMaxima">Profundidad a la que llega el daño, refiriendonos al contenido del dañable</param>
		public SubobjetivoDaño(IDañable _objetivo, bool _dañarContenido, int _profundidadMaxima)
		{
			objetivo          = _objetivo;
			dañarContenido    = _dañarContenido;
			profundidadMaxima = _profundidadMaxima;
		}
	}
}
