using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AppGM.Core
{
	/// <summary>
	/// Representa una lista de <typeparamref name="TItems"/> ordenable por el usuario
	/// </summary>
	/// <typeparam name="TItems">Tipo de los <see cref="ViewModelListaOrdenableItem{TItem}"/> contenidos por la lista</typeparam>
	/// <typeparam name="TContenido">Tipo del contenido que alamacenan los <typeparamref name="TItems"/></typeparam>
	public class ViewModelListaOrdenable<TItems, TContenido> : ViewModel, IEnumerable<TItems>

		where TItems: ViewModelListaOrdenableItem<TItems, TContenido>
	{
		public readonly Func<TContenido, ViewModelListaOrdenable<TItems, TContenido>, TItems> fabricaItems;

		/// <summary>
		/// Items contenidos por la lista
		/// </summary>
		public ViewModelListaDeElementos<TItems> Items { get; set; } =
			new ViewModelListaDeElementos<TItems>();

		/// <summary>
		/// Constructor por defecto
		/// </summary>
		public ViewModelListaOrdenable(Func<TContenido, ViewModelListaOrdenable<TItems, TContenido>, TItems> _fabricaItems)
		{
			fabricaItems = _fabricaItems;

			if(fabricaItems is null)
				SistemaPrincipal.LoggerGlobal.LogCrash($"{nameof(_fabricaItems)} no puede ser null");
		}

		/// <summary>
		/// Constructor que toma <paramref name="_items"/> con los que inicializar esta coleccion
		/// </summary>
		/// <param name="_items">Items con los que inicializar esta lista</param>
		public ViewModelListaOrdenable(IEnumerable<TContenido> _items, Func<TContenido, ViewModelListaOrdenable<TItems, TContenido>, TItems> _fabricaItems)

			:this(_fabricaItems)
		{
			if(_items is null)
				SistemaPrincipal.LoggerGlobal.LogCrash($"{nameof(_items)} no puede ser null");

			_items = _items.ToList();

			foreach (var item in _items)
			{
				Items.Add(fabricaItems(item, this));
			}
		}

		#region Metodos

		/// <summary>
		/// Modifica la posicion en la lista de un <paramref name="item"/>
		/// </summary>
		/// <param name="item">Item cuya posicion modificar</param>
		/// <param name="nuevoIndice">Indice en el que colocar el item</param>
		public int ModificarPosicionItem(TItems item, int nuevoIndice)
		{
			//Si el nuevo indice cae fuera de la lista, nos pegamos la vuelta
			if (nuevoIndice < 0 || nuevoIndice >= Items.Count)
				return -1;

			var indiceActualItem = Items.Elementos.IndexOf(item);

			//Si el item no existe en la lista o es igual al indice actual del item, nos pegamos la vuelta
			if (indiceActualItem < 0 || nuevoIndice == indiceActualItem)
				return -1;

			//Intercambiamos la posicion de los items
			Items[indiceActualItem] = Items[nuevoIndice];
			Items[nuevoIndice] = item;

			return nuevoIndice;
		}

		/// <summary>
		/// Añade un nuevo <typeparamref name="TItems"/> a la lista
		/// </summary>
		/// <param name="item"><typeparamref name="TItems"/> que añadir</param>
		public void Add(TContenido item) => Items.Add(fabricaItems(item, this));

		/// <summary>
		/// Quita un <typeparamref name="TItems"/> de la lista
		/// </summary>
		/// <param name="item"><typeparamref name="TItems"/> que quitar</param>
		public void Remove(TContenido item) => Items.RemoveFirst(i => ReferenceEquals(i.Contenido, item));

		public IEnumerator<TItems> GetEnumerator() => Items.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator(); 

		#endregion
	}
}