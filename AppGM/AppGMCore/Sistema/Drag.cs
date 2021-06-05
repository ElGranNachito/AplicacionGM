using AppGM.Core.Delegados;

namespace AppGM.Core
{
	/// <summary>
	/// Clase que representa un evento 
	/// </summary>
	public class Drag : BaseViewModel
	{
		/// <summary>
		/// Pos del control del drag con respecto a su contenedor en el eje X
		/// </summary>
		public double PosX { get; set; }

		/// <summary>
		/// Pos del control del drag con respecto a su contenedor en el eje Y
		/// </summary>
		public double PosY { get; set; }

		public Grosor OffsetControl { get; set; }

		/// <summary>
		/// Viewmodel del contenido del control del drag
		/// </summary>
		public BaseViewModel ViewModelContenido { get; set; }

		/// <summary>
		/// Evento que se dispara cuando comienza un drag
		/// </summary>
		public event DDrag OnComienzoDrag = delegate{};

		/// <summary>
		/// Evento que se dispara cuan un drag finaliza
		/// </summary>
		public event DDrag OnFinDrag      = delegate {};

		/// <summary>
		/// Comienza un drag
		/// </summary>
		/// <param name="vm"><see cref="BaseViewModel"/> del contenido del control</param>
		public void ComenzarDrag(BaseViewModel vm)
		{
			ViewModelContenido = vm;

			OnComienzoDrag(vm);

			EventoVentana eventoMouseSoltado = null;

			eventoMouseSoltado = ventana =>
			{
				OnFinDrag(ViewModelContenido);

				ViewModelContenido = null;

				SistemaPrincipal.Aplicacion.VentanaActual.OnMouseUp -= eventoMouseSoltado;
			};

			SistemaPrincipal.Aplicacion.VentanaActual.OnMouseUp += eventoMouseSoltado;
		}
	}
}
