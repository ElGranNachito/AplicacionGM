using System;
using System.Threading.Tasks;

namespace AppGM.Core
{
	public delegate void EventoVentana(IVentana ventana);

	public delegate void DataContextContenidoCambioHandler(IVentana vetnana, ViewModel vmAnterior, ViewModel vmActual);

	/// <summary>
	/// Interface que abstrae funcione comunes y propiedades de una ventana
	/// </summary>
	public interface IVentana
	{
		/// <summary>
		/// Titulo de la ventana
		/// </summary>
		string TituloVentana { get; set; }

		/// <summary>
		/// Indica si actualmente hay una ventana de mensaje abierta y la ventana actual deberia
		/// esperar a que este se cierre
		/// </summary>
		bool DebeEsperarCierreDeMensaje { get; set; }

		/// <summary>
		/// Indica si esta es actualmente la ventana siendo utilizada por el usuario
		/// </summary>
		bool EsVentanaActual { get; }

		/// <summary>
		/// <see cref="ViewModel"/> actual de la ventana
		/// </summary>
		ViewModel DataContext { get; set; }

		/// <summary>
		/// <see cref="ViewModel"/> del contenido actual de la ventana
		/// </summary>
		ViewModel DataContextContenido { get; set; }

		/// <summary>
		/// Ventana de popup
		/// </summary>
		Lazy<IVentanaMensaje> VentanaMensaje { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <returns><see cref="object"/> que referencia la clase subyacente de la ventana</returns>
		object ObtenerInstanciaVentana();

		/// <summary>
		/// Cierra la ventana
		/// </summary>
		void CerrarVentana();

		/// <summary>
		/// Maximiza la ventana
		/// </summary>
		void Maximizar();

		/// <summary>
		/// Minimiza la ventana
		/// </summary>
		void Minimizar();

		/// <summary>
		/// Coloca la ventana en estado normal, es decir, ni maximizado ni minimizado
		/// </summary>
		void Normalizar();

		/// <summary>
		/// Establece el tamaño de la ventana
		/// </summary>
		/// <param name="nuevoTamaño"><see cref="Vector2"/> representando el nuevo tamaño de la ventana</param>
		void EstablecerTamaño(Vector2 nuevoTamaño);

		/// <summary>
		/// Establece el tamaño de la ventana en el eje x sin modificar el eje y
		/// </summary>
		/// <param name="x">Nuevo tamaño en el eje x</param>
		void EstablecerTamañoX(float x);

		/// <summary>
		/// Establece el tamaño de la ventana en el eje y sin modificar el eje x
		/// </summary>
		/// <param name="y">Nuevo tamaño en el eje y</param>
		void EstablecerTamañoY(float y);

		/// <summary>
		/// Indica si la ventana esta maximizada
		/// </summary>
		/// <returns><see cref="bool"/> indicando si la ventana esta maximizada</returns>
		bool EstaMaximizada();

		/// <summary>
		/// Obtiene la ventana de mensajes de esta ventana
		/// </summary>
		/// <returns>Instancia de la ventana de mensajes</returns>
		IVentanaMensaje ObtenerVentanaMensaje();

		/// <summary>
		/// 
		/// </summary>
		/// <returns><see cref="Vector2"/> que representa la posicion del mouse respecto a la ventana</returns>
		Vector2 ObtenerPosicionDelMouse();

		/// <summary>
		/// 
		/// </summary>
		/// <returns><see cref="Vector2"/> que representa el tamaño de la ventana</returns>
		Vector2 ObtenerTamaño();

		/// <summary>
		/// Muestra un mensaje sobre esta ventana
		/// </summary>
		/// <param name="vm">View model del contenido el mensaje</param>
		/// <param name="esperarCierre">Si el valor es <see cref="true"/>la ventana actual quedara bloqueada hasta que el mensaje se cierre</param>
		Task<EResultadoViewModel> MostrarMensaje(ViewModelConResultadoBase vm, string titulo, bool esperarCierre, int alto, int ancho);

		event EventoVentana OnTamañoModificado;
		event EventoVentana OnEstadoModificado;
		event EventoVentana OnTituloModificado;
		event EventoVentana OnMouseMovido;
		event EventoVentana OnMouseDown;
		event EventoVentana OnMouseUp;
		event EventoVentana OnVentanaCerrada;
		event EventoVentana OnVentanaAbierta;
		event EventoVentana OnFotogramaActualizado;
		event DataContextContenidoCambioHandler OnDataContextContenidoModificado;
	}

	/// <summary>
	/// Interfaz que amplia <see cref="IVentana"/> para implementar funcionalidad de popups
	/// </summary>
	public interface IVentanaMensaje : IVentana
	{
		/// <summary>
		/// Muestra la el mensaje en pantalla por sobre la ventana principal
		/// </summary>
		/// <param name="vm">View model del contenido el mensaje</param>
		/// <param name="esperarCierre">Si el valor es <see cref="true"/>la ventana principal queda bloqueada hasta que el mensaje se cierre</param>
		Task<EResultadoViewModel> Mostrar(ViewModelConResultadoBase vm, string titulo, bool esperarCierre, int alto, int ancho);

		/// <summary>
		/// Establece el viewmodel de la ventana
		/// </summary>
		/// <param name="nuevoVM">Nuevo viewmodel</param>
		void EstablecerViewModel(ViewModelConResultadoBase nuevoVM);

		/// <summary>
		/// Ventana de la que depende este mensaje
		/// </summary>
		public IVentana VentanaPadre { get; set; }

		bool EstaAbierta { get; }
	}
}