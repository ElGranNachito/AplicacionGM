namespace AppGM.Core
{
	/// <summary>
	/// Argumentos para una tirada de stat
	/// </summary>
	public class ArgumentosTirada
	{
		/// <summary>
		/// Multiplicador de especialidad
		/// </summary>
		public int multiplicadorEspecialidad;

		/// <summary>
		/// Modificador que se aplicara al resultado
		/// </summary>
		public int modificador;

		/// <summary>
		/// Stat de la que depende la tirada
		/// </summary>
		public EStat stat;
	}

	/// <summary>
	/// Argumentos para una tirada personalizada
	/// </summary>
	public class ArgumentosTiradaPersonalizada : ArgumentosTirada
	{
		/// <summary>
		/// Argumento extra de la tirada
		/// </summary>
		public string argumentoExtra;

		/// <summary>
		/// Controlador del personaje que realiza la tirada
		/// </summary>
		public ControladorPersonaje personaje;

		/// <summary>
		/// Controlador del contenedor de esta tirada
		/// </summary>
		public ControladorBase controlador;
	}

	/// <summary>
	/// Argumentos para una tirada de daño
	/// </summary>
	public class ArgumentosTiradaDaño : ArgumentosTiradaPersonalizada
	{
		/// <summary>
		/// Multiplicador de daño de la tirada
		/// </summary>
		public float multiplicador;

		/// <summary>
		/// Mano utilizada para infligir el daño
		/// </summary>
		public EManoUtilizada manoUtilizada;

		/// <summary>
		/// Objetivo del daño
		/// </summary>
		public IDañable objetivo;
	}
}