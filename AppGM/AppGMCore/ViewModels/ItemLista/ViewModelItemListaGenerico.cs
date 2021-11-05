using System;
using System.Collections.Generic;

namespace AppGM.Core
{
	public abstract class ViewModelItemListaGenerico<TViewModel> : ViewModelItemListaBase
		where TViewModel : ViewModelItemListaGenerico<TViewModel>
	{
		#region Eventos

		/// <summary>
		/// Evento que se dispara cuando este elemento es eliminado
		/// </summary>
		public event Action<TViewModel> OnItemEliminado = delegate { };

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_titulo">Titulo del item</param>
		public ViewModelItemListaGenerico(string _titulo)
			:base(_titulo) {} 

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

			ConfigurarEventoItemEliminado(controlador.Modelo);
		}

		/// <summary>
		/// Configura el evento <see cref="OnItemEliminado"/> para que se dispare cuando el <see cref="Controlador"/> es eliminado
		/// </summary>
		/// <param name="modelo">Modelo para el que se configurara el evento</param>
		protected void ConfigurarEventoItemEliminado(ModeloBase modelo)
		{
			mModeloEliminadoHandler = m =>
			{
				OnItemEliminado((TViewModel)this);

				modelo.OnModeloEliminado -= mModeloEliminadoHandler;
				mModeloEliminadoHandler = null;
			};

			modelo.OnModeloEliminado += mModeloEliminadoHandler;
		}

		/// <summary>
		/// Desubscribe <see cref="mModeloEliminadoHandler"/> del evento de item eliminado de <paramref name="modelo"/>
		/// </summary>
		/// <param name="modelo">Modelo del que se quitara el handler</param>
		protected void QuitarHandlerEventoItemEliminado(ModeloBase modelo) => modelo.OnModeloEliminado -= mModeloEliminadoHandler;

		/// <summary>
		/// Crea botones con los contenidos "Editar" y "Eliminar"
		/// </summary>
		/// <param name="_accionBotonEditar">Delegado que se ejecutara cuando el boton editar sea presionado</param>
		/// <param name="_accionBotonEliminar">Delegado que se ejecutara cuando el boton eliminar sea presionado</param>
		protected void CrearBotonesParaEditarYEliminar(Action _accionBotonEditar, Action _accionBotonEliminar)
		{
			GruposDeBotones.Add(new ViewModelGrupoBotones(new List<ViewModelBoton>
			{
				new ViewModelBoton(_accionBotonEditar, ViewModelBoton.NombresComunes.Editar, ViewModelBoton.NombresComunes.Editar, this),
				new ViewModelBoton(_accionBotonEliminar, ViewModelBoton.NombresComunes.Eliminar, ViewModelBoton.NombresComunes.Eliminar, this)
			}));
		}

		#endregion
	}
}