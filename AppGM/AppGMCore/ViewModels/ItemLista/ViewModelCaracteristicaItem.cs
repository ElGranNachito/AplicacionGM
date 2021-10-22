namespace AppGM.Core
{
	/// <summary>
	/// Representa una caracteristica de un <see cref="ViewModelItemListaGenerico{TViewModel}"/>
	/// </summary>
	public class ViewModelCaracteristicaItem : ViewModel
	{
		#region Propiedades

		/// <summary>
		/// Titulo de la caracteristica
		/// </summary>
		public string Titulo { get; set; }

		/// <summary>
		/// Valor de la caracteristica
		/// </summary>
		public string Valor { get; set; }

		#endregion
	}
}