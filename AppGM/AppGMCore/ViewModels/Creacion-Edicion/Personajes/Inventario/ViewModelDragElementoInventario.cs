using System.Collections.Generic;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel que representa el widget de drag creado al arrastrar items
	/// </summary>
	public class ViewModelDragElementoInventario : ViewModel
	{
		/// <summary>
		/// Elementos seleccionados
		/// </summary>
		private List<ViewModelElementoArbolItemInventario> mElementosSeleccionados { get; set; }

		/// <summary>
		/// Descripcion del contenido del drag
		/// </summary>
		public string DescripcionContenido
		{
			get
			{
				if (mElementosSeleccionados == null || mElementosSeleccionados.Count == 0)
					return string.Empty;

				int cantidadItemsSeleccionados = 0;
				int cantidadPartesDelCuerpoSeleccionadas = 0;

				//Contamos la cantidad de elementos seleccionados de cada tipo
				mElementosSeleccionados.ForEach(e =>
				{
					if (e.EsItem)
						cantidadItemsSeleccionados++;

					cantidadPartesDelCuerpoSeleccionadas++;
				});

				//Si solo hay items seleccionados...
				if (cantidadItemsSeleccionados > 0 && cantidadPartesDelCuerpoSeleccionadas == 0)
				{
					//Si hay varios items seleccionados mostramos la cantidad
					if (cantidadItemsSeleccionados > 1)
						return $"{cantidadItemsSeleccionados} items";

					//Si solo hay un item seleccionado entonces mostramos su descripcion
					return mElementosSeleccionados[0].DescripcionSlot;
				}
				//Si solo hay partes del cuerpo seleccionadas...
				else if (cantidadPartesDelCuerpoSeleccionadas > 0 && cantidadItemsSeleccionados == 0)
				{
					//Si hay varias partes del cuerpo seleccionadas mostramos la cantidad
					if (cantidadPartesDelCuerpoSeleccionadas > 1)
						return $"{cantidadPartesDelCuerpoSeleccionadas} partes del cuerpo";

					//Si solo hay una parte del cuerpo seleccionada entonces mostramos su descripcion
					return mElementosSeleccionados[0].DescripcionSlot;
				}

				//Si no se cumplieron las anteriores condiciones entonces solo mostramos la cantidad de elementos seleccionados
				return $"{mElementosSeleccionados.Count} elementos";
			}
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_elementosDrag">Elementos siendo dragueados</param>
		public ViewModelDragElementoInventario(List<ViewModelElementoArbolItemInventario> _elementosDrag)
		{
			mElementosSeleccionados = _elementosDrag;

			if(mElementosSeleccionados == null)
				SistemaPrincipal.LoggerGlobal.Log($"{_elementosDrag} es null", ESeveridad.Error);
		}
	}
}