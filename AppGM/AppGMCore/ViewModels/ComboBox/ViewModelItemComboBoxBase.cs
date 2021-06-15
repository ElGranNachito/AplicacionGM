

namespace AppGM.Core
{
	/// <summary>
	/// <see cref="ViewModel"/> base para un control que aparece en las listas
	/// de las combo boxes
	/// </summary>
	/// <typeparam name="TipoValor"></typeparam>
	public class ViewModelItemComboBoxBase<TipoValor> : ViewModel
	{
		/// <summary>
		/// Valor que almacena
		/// </summary>
		public TipoValor valor;

		/// <summary>
		/// Cadena que se muestra
		/// </summary>
		public string Texto { get; set; }

		/// <summary>
		/// Cadena opcional que aparece en la parte derecha del item
		/// </summary>
		public string TextoExtra { get; set; }
	}
}
