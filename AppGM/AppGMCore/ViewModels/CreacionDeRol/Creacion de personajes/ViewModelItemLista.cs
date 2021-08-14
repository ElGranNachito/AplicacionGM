using System.Windows.Input;

namespace AppGM.Core
{
	/// <summary>
	/// Representa un item en una lista
	/// </summary>
	public class ViewModelItemLista : ViewModel
	{
		public ViewModelListaDeElementos<ViewModelCaracteristicaItem> CaracteristicasItem { get; set; } = new ViewModelListaDeElementos<ViewModelCaracteristicaItem>();

		/// <summary>
		/// Comando que se ejecuta al presionar el boton editar
		/// </summary>
		public ICommand ComandoEditar { get; protected set; }

		/// <summary>
		/// Comando que se ejecuta al presionar el boton eliminar
		/// </summary>
		public ICommand ComandoEliminar { get; protected set; }
	}

	/// <summary>
	/// Representa una caracteristica de un <see cref="ViewModelItemLista"/>
	/// </summary>
	public class ViewModelCaracteristicaItem : ViewModel
	{
		/// <summary>
		/// Titulo de la caracteristica
		/// </summary>
		public string Titulo { get; set; }

		/// <summary>
		/// Valor de la caracteristica
		/// </summary>
		public string Valor { get; set; }
	}
}