using System.IO;
using System.Windows.Input;

namespace AppGM.Core
{
	/// <summary>
	/// Representa un item en una lista
	/// </summary>
	public class ViewModelItemLista : ViewModel
	{
		#region Campos & Propiedades

		/// <summary>
		/// Contiene el valor de <see cref="PathImagen"/>
		/// </summary>
		private string mPathImagen;

		/// <summary>
		/// Lista de todas las caracteristicas de este item
		/// </summary>
		public ViewModelListaDeElementos<ViewModelCaracteristicaItem> CaracteristicasItem { get; set; } = new ViewModelListaDeElementos<ViewModelCaracteristicaItem>();

		/// <summary>
		/// Indica si se deben mostrar los botones
		/// </summary>
		public bool MostrarBotonesLaterales { get; set; }

		/// <summary>
		/// Indica si <see cref="PathImagen"/> es valido
		/// </summary>
		public bool TieneImagen => File.Exists(PathImagen);

		/// <summary>
		/// Texto que se muestra en el boton superior
		/// </summary>
		public string ContenidoBotonSuperior { get; set; }

		/// <summary>
		/// Texto que se muestra en el boton inferior
		/// </summary>
		public string ContenidoBotonInferior { get; set; }

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
		/// Comando que se ejecuta al presionar el boton editar
		/// </summary>
		public ICommand ComandoBotonSuperior { get; protected set; }

		/// <summary>
		/// Comando que se ejecuta al presionar el boton eliminar
		/// </summary>
		public ICommand ComandoBotonInferior { get; protected set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor por defecto
		/// </summary>
		public ViewModelItemLista(bool _mostrarBotonesLaterales = true)
		{
			ContenidoBotonSuperior = "Editar";
			ContenidoBotonInferior = "Eliminar";

			MostrarBotonesLaterales = _mostrarBotonesLaterales;
		} 

		#endregion
	}

	/// <summary>
	/// Item en una lista de <see cref="ControladorBase"/>
	/// </summary>
	public class ViewModelItemListaControlador : ViewModelItemLista
	{
		/// <summary>
		/// Controlador representado por este item
		/// </summary>
		public readonly ControladorBase controlador;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_controlador">Controlador contenido en este item</param>
		public ViewModelItemListaControlador(ControladorBase _controlador, bool _mostrarBotonesLaterales = true)
			:base(_mostrarBotonesLaterales)
		{
			controlador = _controlador;
		}
	}

	/// <summary>
	/// Representa una caracteristica de un <see cref="ViewModelItemLista"/>
	/// </summary>
	public class ViewModelCaracteristicaItem : ViewModel
	{
		#region Propiedades

		/// <summary>
		/// Titulo de la caracteristica
		/// </summary>
		public string Titulo { get; set; }

		/// <summary>
		/// Valor de la caracteristica
		/// </summary>
		public string Valor { get; set; } 

		#endregion
	}
}