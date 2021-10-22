using System;
using System.IO;
using System.Windows.Input;

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
		/// Contiene el valor de <see cref="ContenidoBotonSuperior"/>
		/// </summary>
		private string mContenidoBotonSuperior;

		/// <summary>
		/// Contiene el valor de <see cref="ContenidoBotonInferior"/>
		/// </summary>
		private string mContenidoBotonInferior;

		/// <summary>
		/// Contiene el valor de <see cref="BotonSuperiorEstaHabilitado"/>
		/// </summary>
		private bool mBotonSuperiorEstaHabilitado = true;

		/// <summary>
		/// Contiene el valor de <see cref="BotonInferiorEstaHabilitado"/>
		/// </summary>
		private bool mBotonInferiorEstaHabilitado = true;

		/// <summary>
		/// Delegado que representa a un metodo encargado de lidiar con el evento de que el modelo
		/// representado por este item sea eliminado
		/// </summary>
		protected Action<ModeloBase> mModeloEliminadoHandler;


		//-----------------------------------------------------PROPIEDADES------------------------------------------------------------


		/// <summary>
		/// Lista de todas las caracteristicas de este item
		/// </summary>
		public ViewModelListaDeElementos<ViewModelCaracteristicaItem> CaracteristicasItem { get; set; } = new ViewModelListaDeElementos<ViewModelCaracteristicaItem>();

		/// <summary>
		/// Indica si se debe mostrar el boton superior
		/// </summary>
		public bool MostrarBotonSuperior => mBotonSuperiorEstaHabilitado && !ContenidoBotonSuperior.IsNullOrWhiteSpace();

		/// <summary>
		/// Indica si se debe mostrar el boton inferior
		/// </summary>
		public bool MostrarBotonInferior => mBotonInferiorEstaHabilitado && !ContenidoBotonInferior.IsNullOrWhiteSpace();

		/// <summary>
		/// Indica si <see cref="PathImagen"/> es valido
		/// </summary>
		public bool TieneImagen => File.Exists(PathImagen);

		/// <summary>
		/// Indica si este item representa a un controlador
		/// </summary>
		public bool TieneControlador => Controlador != null;

		/// <summary>
		/// Indica si el boton superior del control esta habilitado
		/// </summary>
		public bool BotonSuperiorEstaHabilitado
		{
			get => mBotonSuperiorEstaHabilitado;
			set
			{
				if (value == mBotonSuperiorEstaHabilitado)
					return;

				mBotonSuperiorEstaHabilitado = value;

				DispararPropertyChanged(nameof(MostrarBotonSuperior));
			}
		}

		/// <summary>
		/// Indica si el boton inferior del control esta habilitado
		/// </summary>
		public bool BotonInferiorEstaHabilitado
		{
			get => mBotonInferiorEstaHabilitado;
			set
			{
				if (value == mBotonInferiorEstaHabilitado)
					return;

				mBotonInferiorEstaHabilitado = value;

				DispararPropertyChanged(nameof(MostrarBotonInferior));
			}
		}

		/// <summary>
		/// Texto que se muestra en el boton superior
		/// </summary>
		public string ContenidoBotonSuperior
		{
			get => mContenidoBotonSuperior;

			set
			{
				if (value == mContenidoBotonSuperior)
					return;

				mContenidoBotonSuperior = value;

				DispararPropertyChanged(nameof(MostrarBotonSuperior));
			}
		}

		/// <summary>
		/// Texto que se muestra en el boton inferior
		/// </summary>
		public string ContenidoBotonInferior
		{
			get => mContenidoBotonInferior;
			set
			{
				if (value == mContenidoBotonInferior)
					return;

				mContenidoBotonInferior = value;

				DispararPropertyChanged(nameof(MostrarBotonInferior));
			}
		}

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

		/// <summary>
		/// Comando que se ejecuta al presionar el boton editar
		/// </summary>
		public ICommand ComandoBotonSuperior { get; protected set; }

		/// <summary>
		/// Comando que se ejecuta al presionar el boton eliminar
		/// </summary>
		public ICommand ComandoBotonInferior { get; protected set; }

		/// <summary>
		/// Delegado que se ejecuta al invocar <see cref="ComandoBotonSuperior"/>
		/// </summary>
		protected Action mAccionBotonSuperior;

		/// <summary>
		/// Delegado que se ejecuta al invocar <see cref="ComandoBotonInferior"/>
		/// </summary>
		protected Action mAccionBotonInferior;

		#endregion

		#region Metodos

		/// <summary>
		/// Actualiza las <see cref="CaracteristicasItem"/>
		/// </summary>
		protected virtual void ActualizarCaracteristicas() { }

		#endregion
	}
}
