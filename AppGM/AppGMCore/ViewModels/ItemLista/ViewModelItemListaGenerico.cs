using System;

namespace AppGM.Core
{
	public abstract class ViewModelItemListaGenerico<TViewModel> : ViewModelItemListaBase
		where TViewModel : ViewModelItemListaGenerico<TViewModel>
	{
		#region Eventos

		/// <summary>
		/// Evento que se dispara cuando el usuario presiona el boton superior, normalmente boton para editar
		/// </summary>
		public event Action<TViewModel> OnBotonSuperiorPresionado = delegate { };

		/// <summary>
		/// Evento que se dispara cuando el usuario presiona el boton inferior, normalmente boton para eliminar
		/// </summary>
		public event Action<TViewModel> OnBotonInferiorPresionado = delegate { };

		/// <summary>
		/// Evento que se dispara cuando este elemento es eliminado
		/// </summary>
		public event Action<TViewModel> OnItemEliminado = delegate { };

		#endregion

		#region Constructores

		/// <summary>
		/// Constructor por defecto
		/// </summary>
		/// <param name="_mostrarBotonesLaterales">Indica si los botones deben ser visibles</param>
		/// <param name="_contenidoBotonSuperior">Contenido del boton superior</param>
		/// <param name="_contenidoBotonInferior">Contenido del boton inferior</param>
		public ViewModelItemListaGenerico(
			bool _mostrarBotonesLaterales = true,
			string _contenidoBotonSuperior = "Editar",
			string _contenidoBotonInferior = "Eliminar")
		{
			if (_mostrarBotonesLaterales)
			{
				ContenidoBotonSuperior = _contenidoBotonSuperior;
				ContenidoBotonInferior = _contenidoBotonInferior;
			}

			ComandoBotonSuperior = new Comando(() =>
			{
				mAccionBotonSuperior?.Invoke();

				OnBotonSuperiorPresionado((TViewModel)this);
			});

			ComandoBotonInferior = new Comando(() =>
			{
				mAccionBotonInferior?.Invoke();

				OnBotonInferiorPresionado((TViewModel)this);
			});
		}

		/// <summary>
		/// Constructor que permite proveer lambdas que se ejecutaran al presionar cada boton
		/// </summary>
		/// <param name="_accionBotonSuperior">Lambda que se ejecutara al presionar el boton superior</param>
		/// <param name="_accionBotonInferior">Lambda que se ejecutara al presionar el boton inferior</param>
		/// <param name="_contenidoBotonSuperior">Contenido del boton superior</param>
		/// <param name="_contenidoBotonInferior">Contenido del boton inferior</param>
		public ViewModelItemListaGenerico(
			Action _accionBotonSuperior,
			Action _accionBotonInferior,
			string _contenidoBotonSuperior = "Editar",
			string _contenidoBotonInferior = "Eliminar")

			: this(true, _contenidoBotonSuperior, _contenidoBotonInferior)
		{
			mAccionBotonSuperior = _accionBotonSuperior;
			mAccionBotonInferior = _accionBotonInferior;
		}

		#endregion

		#region Metodos

		/// <summary>
		/// Configura el evento <see cref="OnItemEliminado"/> para que se dispare cuando el <see cref="Controlador"/> es eliminado
		/// </summary>
		/// <param name="controlador">Controlador que utilizar para configurar el evento. Si se deja en null se defaultea al controlador de este item</param>
		protected void ConfigurarEventoItemEliminado(ControladorBase controlador = null)
		{
			controlador ??= Controlador;

			if (controlador == null)
			{
				SistemaPrincipal.LoggerGlobal.LogCrash($"{nameof(controlador)} fue null");
			}

			mModeloEliminadoHandler = m =>
			{
				OnItemEliminado((TViewModel)this);

				controlador.Modelo.OnModeloEliminado -= mModeloEliminadoHandler;
				mModeloEliminadoHandler = null;
			};

			controlador.Modelo.OnModeloEliminado += mModeloEliminadoHandler;
		}

		#endregion
	}
}
