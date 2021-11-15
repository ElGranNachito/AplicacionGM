namespace AppGM.Core
{
	/// <summary>
	/// Clase base un viewmodel se leccion de controlador
	/// </summary>
	public abstract class ViewModelSeleccionDeControladorBase : ViewModelConResultado<ViewModelSeleccionDeControladorBase>
	{
		/// <summary>
		/// Contiene el vm del controlador actualmente seleccionado
		/// </summary>
		public ViewModelItemListaBase ItemSeleccionado { get; set; }
	}
}