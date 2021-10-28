using System.Collections.Generic;

namespace AppGM.Core
{
	/// <summary>
	/// Argumentos de un evento de drag de varios elementos
	/// </summary>
	public record ArgumentosDragAndDropMultiple : ArgumentosDragAndDropBase
	{
		/// <summary>
		/// Contenido del drag and drop
		/// </summary>
		public List<IDrageable> contenido;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_contenido">Contenido del drag and drop</param>
		/// <param name="_args">Diccionario con los argumentos del drag</param>
		public ArgumentosDragAndDropMultiple(List<IDrageable> _contenido, Dictionary<int, object> _args)
			: base(_args, ETipoDrag.Multiple)
		{
			contenido = _contenido;
		}
	}
}
