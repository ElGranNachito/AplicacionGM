namespace AppGM.Core
{
	/// <summary>
	/// Enum que indica el estado de portacion de un <see cref="ModeloItem"/>
	/// </summary>
	public enum EEstadoPortacion
	{
		/// <summary>
		/// El item esta en casita o algun otro lugar
		/// </summary>
		EnCasita,

		/// <summary>
		/// El item esta siendo transportado
		/// </summary>
		Transportado,

		/// <summary>
		/// El item esta equipado
		/// </summary>
		Equipado
	}
}
