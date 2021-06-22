using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace AppGM.Core
{
	/// <summary>
	/// Lista generica de <see cref="TipoElementos"/>
	/// </summary>
	/// <typeparam name="TipoElementos"><see cref="ViewModel"/></typeparam>
	public class ViewModelListaDeElementos<TipoElementos> : IEnumerable
		where TipoElementos : ViewModel
	{
		/// <summary>
		/// <see cref="ObservableCollection{T}"/> de <see cref="TipoElementos"/> contenidos
		/// </summary>
		public ObservableCollection<TipoElementos> Elementos { get; set; } = new ObservableCollection<TipoElementos>();

		/// <summary>
		/// Cantidad de elementos en <see cref="Elementos"/>
		/// </summary>
		public int Count => Elementos.Count;

		/// <summary>
		/// Constructor por defecto
		/// </summary>
		public ViewModelListaDeElementos(){}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_elementos"><see cref="IEnumerable{T}"/> de <see cref="TipoElementos"/> para inicializar <see cref="Elementos"/></param>
		public ViewModelListaDeElementos(IEnumerable<TipoElementos> _elementos)
		{
			foreach (var elemento in _elementos)
				Elementos.Add(elemento);
		}

		/// <summary>
		/// Añade un <paramref name="elemento"/> a <see cref="Elementos"/>
		/// </summary>
		/// <param name="elemento"><see cref="TipoElementos"/> que añadir</param>
		public void Add(TipoElementos elemento) => Elementos.Add(elemento);

		/// <summary>
		/// Añade un <paramref name="elemento"/> a <see cref="Elementos"/> en <paramref name="indice"/>
		/// </summary>
		/// <param name="indice">IndiceZ donde añadir el elemento</param>
		/// <param name="elemento">Elemento que añadir</param>
		public void Insert(int indice, TipoElementos elemento) => Elementos.Insert(indice, elemento);

		/// <summary>
		/// Quita un <paramref name="elemento"/> de <see cref="Elementos"/>
		/// </summary>
		/// <param name="elemento"><see cref="TipoElementos"/> que quitar</param>
		/// <returns><see cref="bool"/> indicando si el elemento fue removido</returns>
		public bool Remove(TipoElementos elemento) => Elementos.Remove(elemento);

		/// <summary>
		/// Devuelve un <see cref="bool"/> indicando si el <paramref name="elemento"/> existe en <see cref="Elementos"/>
		/// </summary>
		/// <param name="elemento"><see cref="TipoElementos"/> que quitar</param>
		/// <returns><see cref="bool"/> indicando si el elemento existe</returns>
		public bool Contiene(TipoElementos elemento) => Elementos.Contains(elemento);

		public IEnumerator GetEnumerator() => Elementos.GetEnumerator();

		/// <summary>
		/// Devuelve el <see cref="TipoElementos"/> contenido en <see cref="Elementos"/>
		/// en el <paramref name="indice"/>
		/// </summary>
		/// <param name="indice">IndiceZ del elemento</param>
		/// <returns></returns>
		[MaybeNull]
		public TipoElementos this[int indice]
		{
			get
			{
				if (indice >= Elementos.Count || indice < 0)
					return null;

				return Elementos[indice];
			}

			set
			{
				if (indice >= Elementos.Count || indice < 0)
					return;

				Elementos[indice] = value;
			}
		}
	}

	/// <summary>
	/// Version no generica de <see cref="ViewModelListaDeElementos{T}"/>
	/// </summary>
	public class ViewModelListaDeElementos : ViewModelListaDeElementos<ViewModel>
	{
		public ViewModelListaDeElementos() {}
		public ViewModelListaDeElementos(IEnumerable<ViewModel> _elementos)
			:base(_elementos) {}
	}
}