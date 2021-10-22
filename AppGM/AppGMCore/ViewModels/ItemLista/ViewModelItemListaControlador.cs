using System;

namespace AppGM.Core
{
	/// <summary>
	/// Item en una lista de <see cref="ControladorBase"/>
	/// </summary>
	public class ViewModelItemListaControlador<TViewModel, TControlador> : ViewModelItemListaGenerico<TViewModel>
		where TViewModel : ViewModelItemListaControlador<TViewModel, TControlador>
		where TControlador : ControladorBase
	{
		/// <summary>
		/// Contiene el valor de <see cref="ControladorGenerico"/>
		/// </summary>
		private TControlador mControladorGenerico;

		/// <summary>
		/// Variable representada
		/// </summary>
		public TControlador ControladorGenerico
		{
			get => mControladorGenerico;
			set
			{
				if (value != Controlador)
				{
					Controlador.Modelo.OnModeloEliminado -= mModeloEliminadoHandler;

					ConfigurarEventoItemEliminado(value);
				}

				//No revisamos que el nuevo valor sea distinto porque aun si es el mismo nos intresa
				//llamar a ActualizarCaracteristicas en caso de que alguno de los campos/propiedades
				//de el controlador haya sido modificado

				mControladorGenerico = value;
				Controlador = value;

				ActualizarCaracteristicas();
			}
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_controlador">Controlador contenido en este item</param>
		/// <param name="_mostrarBotonesLaterales">Indica si los botones deben ser visibles</param>
		/// <param name="_contenidoBotonSuperior">Contenido del boton superior</param>
		/// <param name="_contenidoBotonInferior">Contenido del boton inferior</param>
		public ViewModelItemListaControlador(
			ControladorBase _controlador,
			bool _mostrarBotonesLaterales = true,
			string _contenidoBotonSuperior = "Editar",
			string _contenidoBotonInferior = "Eliminar")

			: base(_mostrarBotonesLaterales, _contenidoBotonSuperior, _contenidoBotonInferior)
		{
			Controlador = _controlador;

			ConfigurarEventoItemEliminado();
		}

		/// <summary>
		/// Constructor que permite proveer lambdas que se ejecutaran al presionar cada boton
		/// </summary>
		/// <param name="_controlador">Controlador contenido en este item</param>
		/// <param name="_accionBotonSuperior">Lambda que se ejecutara al presionar el boton superior</param>
		/// <param name="_accionBotonInferior">Lambda que se ejecutara al presionar el boton inferior</param>
		/// <param name="_contenidoBotonSuperior">Contenido del boton superior</param>
		/// <param name="_contenidoBotonInferior">Contenido del boton inferior</param>
		public ViewModelItemListaControlador(
			ControladorBase _controlador,
			Action _accionBotonSuperior,
			Action _accionBotonInferior,
			string _contenidoBotonSuperior = "Editar",
			string _contenidoBotonInferior = "Eliminar")

			: base(_accionBotonSuperior, _accionBotonInferior, _contenidoBotonSuperior, _contenidoBotonInferior)
		{
			Controlador = _controlador;

			ConfigurarEventoItemEliminado();
		}
	}
}