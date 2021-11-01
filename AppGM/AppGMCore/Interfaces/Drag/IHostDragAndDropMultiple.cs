using System.Collections.Generic;

namespace AppGM.Core
{
	/// <summary>
	/// Interfaz que debe ser implementada por aquellos viewmodels que contengan elementos con capacidad de drag & drop multiple
	/// </summary>
	public interface IHostDragAndDropMultiple
	{
		/// <summary>
		/// Elementos actualmente seleccionados
		/// </summary>
		public List<IDrageableMultiple> ElementosSeleccionados { get; set; }

		/// <summary>
		/// Seleccionar un <paramref name="elemento"/>
		/// </summary>
		/// <param name="elemento">Elemento que seleccionar</param>
		public void SeleccionarElemento(IDrageableMultiple elemento);

		/// <summary>
		/// Seleccionar un <paramref name="elemento"/>
		/// </summary>
		/// <param name="elemento">Elemento que seleccionar</param>
		public void DeseleccionarElemento(IDrageableMultiple elemento);
	}
}
