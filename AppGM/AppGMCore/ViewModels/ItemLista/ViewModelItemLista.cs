using System;

namespace AppGM.Core
{
	/// <summary>
	/// Representa un item en una lista
	/// </summary>
	public class ViewModelItemLista : ViewModelItemListaGenerico<ViewModelItemLista>
	{
		/// <summary>
		/// Constructor por defecto
		/// </summary>
		/// <param name="_mostrarBotonesLaterales">Indica si los botones deben ser visibles</param>
		/// <param name="_contenidoBotonSuperior">Contenido del boton superior</param>
		/// <param name="_contenidoBotonInferior">Contenido del boton inferior</param>
		public ViewModelItemLista(
			bool _mostrarBotonesLaterales = true,
			string _contenidoBotonSuperior = "Editar",
			string _contenidoBotonInferior = "Eliminar")

			: base(_mostrarBotonesLaterales, _contenidoBotonSuperior, _contenidoBotonInferior) {}

		/// <summary>
		/// Constructor que permite proveer lambdas que se ejecutaran al presionar cada boton
		/// </summary>
		/// <param name="_accionBotonSuperior">Lambda que se ejecutara al presionar el boton superior</param>
		/// <param name="_accionBotonInferior">Lambda que se ejecutara al presionar el boton inferior</param>
		/// <param name="_contenidoBotonSuperior">Contenido del boton superior</param>
		/// <param name="_contenidoBotonInferior">Contenido del boton inferior</param>
		public ViewModelItemLista(
			Action _accionBotonSuperior,
			Action _accionBotonInferior,
			string _contenidoBotonSuperior = "Editar",
			string _contenidoBotonInferior = "Eliminar")

			: base(_accionBotonSuperior, _accionBotonInferior, _contenidoBotonSuperior, _contenidoBotonInferior){}
	}
}