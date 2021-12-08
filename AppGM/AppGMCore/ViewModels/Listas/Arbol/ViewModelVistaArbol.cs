using System;
using System.Collections.Generic;

namespace AppGM.Core
{
	public class ViewModelVistaArbol<TViewModelElementos, TContenidoElementos> : ViewModel, IHostDragAndDropMultiple

		where TViewModelElementos: ViewModelElementoArbolBase
	{
		/// <summary>
		/// Evento que se dispara cuando el elemento del arbol actualmente seleccionado cambia
		/// </summary>
		public event Action<ViewModelVistaArbol<TViewModelElementos, TContenidoElementos>, TViewModelElementos> OnElementoSeleccionadoCambio = delegate { };

		public event Action<ViewModelVistaArbol<TViewModelElementos, TContenidoElementos>, TViewModelElementos> OnElementoDeseleccionado = delegate { };

		public ViewModelListaDeElementos<TViewModelElementos> Hijos { get; set; } =
			new ViewModelListaDeElementos<TViewModelElementos>();

		public List<IDrageableMultiple> ElementosSeleccionados { get; set; } 
			= new List<IDrageableMultiple>();

		public ViewModelVistaArbol(List<TViewModelElementos> _hijos)
		{
			if(_hijos == null)
				return;

			Hijos.AddRange(_hijos);
		}

		public void SeleccionarElemento(IDrageableMultiple elemento)
		{
			ElementosSeleccionados.Add(elemento);

			OnElementoSeleccionadoCambio(this, (TViewModelElementos)elemento);
		}

		public void DeseleccionarElemento(IDrageableMultiple elemento)
		{
			ElementosSeleccionados.Remove(elemento);

			OnElementoSeleccionadoCambio(this, (TViewModelElementos)elemento);
			OnElementoDeseleccionado(this, (TViewModelElementos)elemento);
		}
	}
}
