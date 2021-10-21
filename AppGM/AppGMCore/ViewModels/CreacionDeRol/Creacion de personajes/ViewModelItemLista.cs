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
				if(value == mBotonSuperiorEstaHabilitado)
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

	public abstract class ViewModelItemListaGenerico<TViewModel> : ViewModelItemListaBase
		where TViewModel: ViewModelItemListaGenerico<TViewModel>
	{
		#region Eventos

		/// <summary>
		/// Evento que se dispara cuando el usuario presiona el boton superior, normalmente boton para editar
		/// </summary>
		public event Action<TViewModel> OnBotonSuperiorPresionado = delegate{};

		/// <summary>
		/// Evento que se dispara cuando el usuario presiona el boton inferior, normalmente boton para eliminar
		/// </summary>
		public event Action<TViewModel> OnBotonInferiorPresionado = delegate {};

		#region Eventos

		/// <summary>
		/// Evento que se dispara cuando este elemento es eliminado
		/// </summary>
		public event Action<TViewModel> OnItemEliminado = delegate { };

		#endregion

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
		{
			ContenidoBotonSuperior = _contenidoBotonSuperior;
			ContenidoBotonInferior = _contenidoBotonInferior;

			mAccionBotonSuperior = _accionBotonSuperior;
			mAccionBotonInferior = _accionBotonInferior;

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

		#endregion

		#region Metodos

		/// <summary>
		/// Configura el evento <see cref="OnItemEliminado"/> para que se dispare cuando el <see cref="Controlador"/> es eliminado
		/// </summary>
		/// <param name="controlador">Controlador que utilizar para configurar el evento. Si se deja en null se defaultea al controlador de este item</param>
		protected void ConfigurarEventoItemEliminado(ControladorBase controlador = null)
		{
			controlador ??= Controlador;

			if(controlador == null)
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
				if(value != Controlador)
				{
					Controlador.Modelo.OnModeloEliminado -= mModeloEliminadoHandler;

					ConfigurarEventoItemEliminado(value);
				}

				//No revisamos que el nuevo valor sea distinto porque aun si es el mismo nos intresa
				//llamar a ActualizarCaracteristicas en caso de que alguno de los campos/propiedades
				//de el controlador haya sido modificado

				mControladorGenerico = value;
				Controlador          = value;

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

	/// <summary>
	/// Representa una caracteristica de un <see cref="ViewModelItemListaGenerico{TViewModel}"/>
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