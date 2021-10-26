using System.Collections.Generic;
using System.Linq;

namespace AppGM.Core
{
	/// <summary>
	/// Interfaz que define metodos para ser implementados por aquellos <see cref="ViewModel"/> que representan un control arrastrable
	/// </summary>
	public interface IDrageableMultiple : IDrageable
	{
		/// <summary>
		/// Elemento que contiene a todos los <see cref="IReceptorDeDragMultiple"/> que participan del drag and drop
		/// </summary>
		public IHostDragAndDropMultiple HostDragAndDrop { get; set; }

		/// <summary>
		/// Funcion llamada cuando este elemento comienza a ser draggeado
		/// </summary>
		/// <param name="args">Argumentos del evento</param>
		public new virtual void OnComienzoDrag(ArgumentosDragAndDropBase args)
		{
			if (args is ArgumentosDragAndDropMultiple argsMultiple)
			{
				HostDragAndDrop.ElementosSeleccionados.Add(this);

				OnComienzoDrag_Impl(argsMultiple);
			}
		}

		/// <summary>
		/// Funcion que se llama cuando este elemento comienza a estar sobre <paramref name="elemento"/>
		/// </summary>
		/// <param name="elemento"><see cref="IReceptorDeDrag"/> sobre el que comienza a estar</param>
		/// <param name="args">Argumentos del evento de drag</param>
		public virtual void OnEntrarAElemento(IReceptorDeDrag elemento, ArgumentosDragAndDropBase args)
		{
			if (args is ArgumentosDragAndDropMultiple argsMultiple && elemento is IReceptorDeDragMultiple elementoReceptorDeDragMultiple)
			{
				HostDragAndDrop.ElementosSeleccionados.Add(this);

				OnEntrarAElemento_Impl(elementoReceptorDeDragMultiple, argsMultiple);
			}
		}

		/// <summary>
		/// Funcion que se llama cuando este elemento deja de estar sobre <paramref name="elemento"/>
		/// </summary>
		/// <param name="elemento"><see cref="IReceptorDeDrag"/> sobre el que deja de estar</param>
		/// <param name="args">Argumentos del evento</param>
		public virtual void OnSalirDeElemento(IReceptorDeDrag elemento, ArgumentosDragAndDropBase args)
		{
			if (args is ArgumentosDragAndDropMultiple argsMultiple && elemento is IReceptorDeDragMultiple elementoReceptorDeDragMultiple)
			{
				HostDragAndDrop.ElementosSeleccionados.Add(this);

				OnSalirDeElemento_Impl(elementoReceptorDeDragMultiple, argsMultiple);
			}
		}

		/// <summary>
		/// Funcion que se llama cuando este elemento deja de estar sobre <paramref name="receptores"/>
		/// </summary>
		/// <param name="receptores"><see cref="IReceptorDeDrag"/> sobre el que se solto</param>
		/// <param name="args">Argumentos del evento</param>
		public new virtual void Soltado(List<IReceptorDeDrag> receptores, ArgumentosDragAndDropBase args)
		{
			if (args is ArgumentosDragAndDropMultiple argsMultiple)
			{
				HostDragAndDrop.ElementosSeleccionados.Remove(this);

				OnFinDrag_Impl(receptores.Cast<IReceptorDeDragMultiple>().ToList(), argsMultiple);
			}
		}

		/// <summary>
		/// Metodo que se llama cuando se comienza a arrastrar este elemento
		/// </summary>
		/// <param name="args">Argumentos del evento</param>
		public void OnComienzoDrag_Impl(ArgumentosDragAndDropMultiple args);

		/// <summary>
		/// Funcion que se llama cuando este elemento comienza a estar sobre <paramref name="elemento"/>
		/// </summary>
		/// <param name="elemento"><see cref="IReceptorDeDrag"/> sobre el que comienza a estar</param>
		/// <param name="args">Argumentos del evento de drag</param>
		public virtual void OnEntrarAElemento_Impl(IReceptorDeDragMultiple elemento, ArgumentosDragAndDropMultiple args){}

		/// <summary>
		/// Funcion que se llama cuando este elemento comienza a estar sobre <paramref name="elemento"/>
		/// </summary>
		/// <param name="elemento"><see cref="IReceptorDeDrag"/> sobre el que comienza a estar</param>
		/// <param name="args">Argumentos del evento de drag</param>
		public virtual void OnSalirDeElemento_Impl(IReceptorDeDragMultiple elemento, ArgumentosDragAndDropMultiple args){}

		/// <summary>
		/// Metodo que se llama cuando se deja de arrastrar este elemento
		/// </summary>
		/// <param name="receptores"><see cref="IReceptorDeDrag"/> sobre el que se solto</param>
		/// <param name="args">Argumentos del evento</param>
		public virtual void OnFinDrag_Impl(List<IReceptorDeDragMultiple> receptores, ArgumentosDragAndDropMultiple args){}
	}
}