using System;

using AppGM.Core.Delegados;

namespace AppGM.Core
{
	public interface IDrageable
	{
		/// <summary>
		/// Funcion llamada cuando este elemento comienza a ser draggeado
		/// </summary>
		public void OnComienzoDrag();

		/// <summary>
		/// Funcion que se llama cuando este elemento comienza a estar sobre <paramref name="elemento"/>
		/// </summary>
		/// <param name="elemento"><see cref="IReceptorDeDrag"/> sobre el que comienza a estar</param>
		public virtual void OnEntrarAElemento(IReceptorDeDrag elemento){}

		/// <summary>
		/// Funcion que se llama cuando este elemento deja de estar sobre <paramref name="elemento"/>
		/// </summary>
		/// <param name="elemento"><see cref="IReceptorDeDrag"/> sobre el que deja de estar</param>
		public virtual void OnSalirDeElemento(IReceptorDeDrag elemento){}

		/// <summary>
		/// Funcion que se llama cuando este elemento deja de estar sobre <paramref name="elemento"/>
		/// </summary>
		/// <param name="elemento"><see cref="IReceptorDeDrag"/> sobre el que se solto</param>
		public virtual void Soltado(IReceptorDeDrag elemento){}
	}
}
