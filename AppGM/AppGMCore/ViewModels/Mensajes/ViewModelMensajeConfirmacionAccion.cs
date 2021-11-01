namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel que representa a un popup de confirmacion
	/// </summary>
	public class ViewModelMensajeConfirmacionAccion : ViewModelConResultado<ViewModelMensajeConfirmacionAccion>
	{
		/// <summary>
		/// Titulo del popup
		/// </summary>
		public string Titulo { get; init; }

		/// <summary>
		/// Mensaje
		/// </summary>
		public string Mensaje { get; init; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_titulo">Titulo del mensaje</param>
		/// <param name="_mensaje">Mensaje</param>
		public ViewModelMensajeConfirmacionAccion(string _titulo, string _mensaje)
		{
			Titulo  = _titulo;
			Mensaje = _mensaje;
		}
	}
}