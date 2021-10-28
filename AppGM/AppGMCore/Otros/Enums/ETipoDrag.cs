namespace AppGM.Core
{
	/// <summary>
	/// Indica el tipo de un evento de drag
	/// </summary>
	public enum ETipoDrag
	{
		/// <summary>
		/// Drag de un solo elemento
		/// </summary>
		Unico = 1,

		/// <summary>
		/// Drag de varios elementos
		/// </summary>
		Multiple = 2,

		/// <summary>
		/// Ningun tipo de drag. Utilizado para indicar que no hay ningun drag activo
		/// </summary>
		Ninguno = 0
	}
}
