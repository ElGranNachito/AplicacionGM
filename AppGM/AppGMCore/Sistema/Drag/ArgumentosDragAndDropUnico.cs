using System.Collections.Generic;

namespace AppGM.Core
{
	/// <summary>
	/// Argumentos de un evento de drag de un solo elemento
	/// </summary>
	public record ArgumentosDragAndDropUnico : ArgumentosDragAndDropBase
	{
		/// <summary>
		/// Contenido del drag and drop
		/// </summary>
		public IDrageable contenido;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_contenido">Contenido del drag and drop</param>
		/// <param name="_args">Diccionario con los argumentos del drag</param>
		public ArgumentosDragAndDropUnico(IDrageable _contenido, Dictionary<int, object> _args)
			:base(_args)
		{
			contenido = _contenido;
		}
	}
}