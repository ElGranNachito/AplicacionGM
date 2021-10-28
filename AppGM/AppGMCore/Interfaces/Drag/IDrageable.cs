using System.Collections.Generic;
using System.Linq;

namespace AppGM.Core
{
	/// <summary>
	/// Interfaz que define metodos para ser implementados por aquellos <see cref="ViewModel"/> que representan un control arrastrable
	/// </summary>
	public interface IDrageable
	{
		/// <summary>
		/// Funcion llamada cuando este elemento comienza a ser draggeado
		/// </summary>
		/// <param name="args">Argumentos del evento</param>
		public virtual void OnComienzoDrag(ArgumentosDragAndDropBase args)
		{
			if (this is IDrageableMultiple drageableMultiple && args is ArgumentosDragAndDropMultiple argsDragMultiple)
			{
				drageableMultiple.OnComienzoDrag_Impl(argsDragMultiple);
			}
		}

		/// <summary>
		/// Funcion que se llama cuando este elemento comienza a estar sobre <paramref name="elemento"/>
		/// </summary>
		/// <param name="elemento"><see cref="IReceptorDeDrag"/> sobre el que comienza a estar</param>
		/// <param name="args">Argumentos del evento de drag</param>
		public virtual void OnEntrarAElemento(IReceptorDeDrag elemento, ArgumentosDragAndDropBase args)
		{
			if (this is IDrageableMultiple drageableMultiple &&
			    args is ArgumentosDragAndDropMultiple argsDragMultiple &&
			    elemento is IReceptorDeDragMultiple receptorDragMultiple)
			{
				drageableMultiple.OnEntrarAElemento_Impl(receptorDragMultiple, argsDragMultiple);
			}
		}

		/// <summary>
		/// Funcion que se llama cuando este elemento deja de estar sobre <paramref name="elemento"/>
		/// </summary>
		/// <param name="elemento"><see cref="IReceptorDeDrag"/> sobre el que deja de estar</param>
		/// <param name="args">Argumentos del evento</param>
		public virtual void OnSalirDeElemento(IReceptorDeDrag elemento, ArgumentosDragAndDropBase args)
		{
			if (this is IDrageableMultiple drageableMultiple &&
			    args is ArgumentosDragAndDropMultiple argsDragMultiple &&
			    elemento is IReceptorDeDragMultiple receptorDragMultiple)
			{
				drageableMultiple.OnSalirDeElemento_Impl(receptorDragMultiple, argsDragMultiple);
			}
		}

		/// <summary>
		/// Funcion que se llama cuando este elemento deja de estar sobre <paramref name="receptores"/>
		/// </summary>
		/// <param name="receptores"><see cref="IReceptorDeDrag"/> sobre el que se solto</param>
		/// <param name="args">Argumentos del evento</param>
		public virtual void Soltado(List<IReceptorDeDrag> receptores, ArgumentosDragAndDropBase args)
		{
			if (this is IDrageableMultiple drageableMultiple &&
			    args is ArgumentosDragAndDropMultiple argsDragMultiple &&
			    receptores.Cast<IReceptorDeDragMultiple>()?.ToList() is {} receptoresDragMultiple)
			{
				drageableMultiple.OnFinDrag_Impl(receptoresDragMultiple, argsDragMultiple);
			}
		}

		/// <summary>
		/// Indica si este elemento puede ser arrastrado
		/// </summary>
		/// <returns><see cref="bool"/> indicando si este elemento puede ser dragueado</returns>
		public bool PuedeSerDragueado();
	}
}
