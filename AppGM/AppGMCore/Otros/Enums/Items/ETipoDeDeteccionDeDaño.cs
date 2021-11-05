namespace AppGM.Core
{
	/// <summary>
	/// Manera en la que se detecta el daño a reducir
	/// </summary>
	public enum ETipoDeDeteccionDeDaño
	{
		/// <summary>
		/// El daño se detecta en base a su <see cref="ETipoDeDaño"/>
		/// </summary>
		TipoDeDaño,

		/// <summary>
		/// El daño se detecta en base a su <see cref="ERango"/>
		/// </summary>
		Rango,

		/// <summary>
		/// El daño se detecta en base a su <see cref="ModeloFuenteDeDaño"/>
		/// </summary>
		FuenteDelDaño,

		/// <summary>
		/// Este deteccion solo funciona con <see cref="ModeloMagia"/> y se basa en su nivel
		/// </summary>
		Nivel
	}
}