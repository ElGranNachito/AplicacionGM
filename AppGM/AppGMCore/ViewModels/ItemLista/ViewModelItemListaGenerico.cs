using System;

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