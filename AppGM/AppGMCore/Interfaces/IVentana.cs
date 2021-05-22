using System.Threading.Tasks;

namespace AppGM.Core
{
	public delegate void EventoVentana(IVentana ventana);

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
		/// 
		/// </summary>
		/// <returns><see cref="bool"/> indicando si la ventana esta maximizada</returns>
		bool EstaMaximizada();

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

		event EventoVentana OnTamañoModificado;
		event EventoVentana OnEstadoModificado;
		event EventoVentana OnTituloModificado;
		event EventoVentana OnMouseMovido;
		event EventoVentana OnMouseDown;
		event EventoVentana OnMouseUp;
		event EventoVentana OnVentanaCerrada;
		event EventoVentana OnVentanaAbierta;
		event EventoVentana OnFotogramaActualizado;
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
		Task Mostrar(ViewModelMensajeBase vm, bool esperarCierre, int alto, int ancho);

		/// <summary>
		/// Establece el viewmodel de la ventana
		/// </summary>
		/// <param name="nuevoVM">Nuevo viewmodel</param>
		void EstablecerViewModel(ViewModelMensajeBase nuevoVM);

		bool EstaAbierta { get; }
		bool DebeEsperarCierre { get; set; }
	}
}