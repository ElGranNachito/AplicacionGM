using System;
using System.IO;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AppGM.Core
{
	public abstract class ViewModelItemListaBase : ViewModel
	{
		#region Campos & Propiedades

		//-------------------------------------------------CAMPOS---------------------------------------------------

		/// <summary>
		/// Contiene el valor de <see cref="PathImagen"/>
		/// </summary>
		private string mPathImagen;

		/// <summary>
		/// Contiene el valor de <see cref="EstaSeleccionado"/>
		/// </summary>
		private bool mEstaSeleccionado;

		/// <summary>
		/// Contiene el valor de <see cref="IndiceGrupoDeBotonesActivo"/>
		/// </summary>
		private int mIndiceGrupoDeBotonesActivo = -1;

		/// <summary>
		/// Delegado que representa a un metodo encargado de lidiar con el evento de que el modelo
		/// representado por este item sea eliminado
		/// </summary>
		protected Action<ModeloBase> mModeloEliminadoHandler;

		//-----------------------------------------------------PROPIEDADES------------------------------------------------------------

		/// <summary>
		/// Obtiene o establece el indice del grupo de botones actualmente activo
		/// </summary>
		public int IndiceGrupoDeBotonesActivo
		{
			get => mIndiceGrupoDeBotonesActivo;
			set
			{
				if (value >= GruposDeBotones.Count)
					return;

				if(mIndiceGrupoDeBotonesActivo != -1)
					GruposDeBotones[mIndiceGrupoDeBotonesActivo].EsVisible = false;

				mIndiceGrupoDeBotonesActivo = value;

				if (mIndiceGrupoDeBotonesActivo != -1)
					GruposDeBotones[mIndiceGrupoDeBotonesActivo].EsVisible = true;
			}
		}

		/// <summary>
		/// Grupos de botones que hay en cada item
		/// </summary>
		public ViewModelListaDeElementos<ViewModelGrupoBotones> GruposDeBotones { get; set; } = new ViewModelListaDeElementos<ViewModelGrupoBotones>();

		/// <summary>
		/// Lista de todas las caracteristicas de este item
		/// </summary>
		public ViewModelListaDeElementos<ViewModelCaracteristicaItem> CaracteristicasItem { get; set; } = new ViewModelListaDeElementos<ViewModelCaracteristicaItem>();

		/// <summary>
		/// Indica si <see cref="PathImagen"/> es valido
		/// </summary>
		public bool TieneImagen => File.Exists(PathImagen);

		/// <summary>
		/// Indica si este item representa a un controlador
		/// </summary>
		public bool TieneControlador => Controlador != null;

		/// <summary>
		/// Indica si el titulo de este item es visible
		/// </summary>
		public bool TituloEsVisible => Titulo.IsNullOrWhiteSpace();

		/// <summary>
		/// Indica si este item esta actualmente seleccionado
		/// </summary>
		public bool EstaSeleccionado { get; set; }

		/// <summary>
		/// Titulo de este item
		/// </summary>
		public string Titulo { get; set; }

		/// <summary>
		/// Ruta completa a la imagen de este item
		/// </summary>
		public string PathImagen
		{
			get => mPathImagen;
			set
			{
				if (value == mPathImagen)
					return;

				mPathImagen = value;

				DispararPropertyChanged(nameof(TieneImagen));
			}
		}

		/// <summary>
		/// Controlador representado por este item
		/// </summary>
		public ControladorBase Controlador { get; set; }

		#endregion

		#region Constructores

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_titulo">Titulo del elemento</param>
		public ViewModelItemListaBase(string _titulo)
		{
			Titulo = _titulo;
		}
		
		#endregion

		#region Metodos

		/// <summary>
		/// Añade un <paramref name="handler"/> al evento <see cref="ViewModelBoton.OnClick"/> de un <see cref="ViewModelBoton"/>
		/// </summary>
		/// <param name="nombreBoton">Nombre del boton al que añadir el <paramref name="handler"/></param>
		/// <param name="handler">Handler que añadir al evento</param>
		/// <returns><see cref="bool"/> indicando si se pudo añadir el handler</returns>
		public bool AñadirHandlerClick(string nombreBoton, Action<ViewModelBoton> handler)
		{
			foreach (var grupo in GruposDeBotones)
			{
				if (grupo.AñadirHandlerClick(nombreBoton, handler))
					return true;
			}

			return false;
		}

		/// <summary>
		/// Quita un <paramref name="handler"/> del evento <see cref="ViewModelBoton.OnClick"/> de un <see cref="ViewModelBoton"/>
		/// </summary>
		/// <param name="nombreBoton">Nombre del boton al que quitar el <paramref name="handler"/></param>
		/// <param name="handler">Handler que quitar del evento</param>
		/// <returns><see cref="bool"/> indicando si se pudo quitar el handler</returns>
		public bool QuitarHandlerClick(string nombreBoton, Action<ViewModelBoton> handler)
		{
			foreach (var grupo in GruposDeBotones)
			{
				if (grupo.QuitarHandlerClick(nombreBoton, handler))
					return true;
			}

			return false;
		}

		/// <summary>
		/// Actualiza las <see cref="CaracteristicasItem"/>
		/// </summary>
		protected virtual void ActualizarCaracteristicas(){}

		/// <summary>
		/// Actualiza los <see cref="GruposDeBotones"/>
		/// </summary>
		protected virtual void ActualizarGruposDeBotones(){}

		#endregion
	}
}