using System;
using System.Collections.Generic;
using CoolLogs;

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
				if (Controlador is not null && value != Controlador)
				{
					Controlador.Modelo.OnModeloEliminado -= mModeloEliminadoHandler;

					ConfigurarEventoItemEliminado(value);
				}

				//No revisamos que el nuevo valor sea distinto porque aun si es el mismo nos intresa
				//llamar a ActualizarCaracteristicas en caso de que alguno de los campos/propiedades
				//de el controlador haya sido modificado

				bool valorAnteriorEraNull = mControladorGenerico == null;

				mControladorGenerico = value;
				Controlador = value;

				if (valorAnteriorEraNull)
				{
					GruposDeBotones.Elementos.Clear();

					ActualizarGruposDeBotones();
				}

				CaracteristicasItem.Elementos.Clear();

				ActualizarCaracteristicas();
			}
		}

		/// <summary>
		/// Constructor por defecto
		/// </summary>
		public ViewModelItemListaControlador(string _titulo = "")
			:base(_titulo) {}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_controlador">Controlador contenido en este item</param>
		public ViewModelItemListaControlador(TControlador _controlador, string _titulo = "")
			:base(_titulo)
		{
			if (_controlador == null)
			{
				SistemaPrincipal.LoggerGlobal.Log($"{nameof(_controlador)} pasado es null!", ESeveridad.Error);

				return;
			}

			ControladorGenerico = _controlador;

			ConfigurarEventoItemEliminado();
		}
	}
}