namespace AppGM.Core
{
	/// <summary>
	/// Interfaz que deben implementar los <see cref="ViewModel"/> que necesiten ser capaces de responder a un
	/// evento de <see cref="Drag"/>
	/// </summary>
	public interface IReceptorDeDrag
	{
		/// <summary>
		/// Indice en el eje Z.
		/// Se utiliza para determinar el orden en el que se reciben los eventos de
		/// <see cref="OnDragEntro"/>, <see cref="OnDragSalio"/>, <see cref="OnDrop"/>
		/// </summary>
		public int IndiceZ { get; set; }

		public virtual void OnDragEntro(ArgumentosDragAndDropBase args)
		{
			if (this is IReceptorDeDragUnico receptorDragUnico && args is ArgumentosDragAndDropUnico argsDragUnico)
			{
				receptorDragUnico.OnDragEntro_Impl(argsDragUnico);
			}
			else if (this is IReceptorDeDragMultiple receptorDragMultiple && args is ArgumentosDragAndDropMultiple argsDragMultiple)
			{
				receptorDragMultiple.OnDragEntro_Impl(argsDragMultiple);
			}
		}

		public virtual void OnDragSalio(ArgumentosDragAndDropBase args)
		{
			if (this is IReceptorDeDragUnico receptorDragUnico && args is ArgumentosDragAndDropUnico argsDragUnico)
			{
				receptorDragUnico.OnDragSalio_Impl(argsDragUnico);
			}
			else if (this is IReceptorDeDragMultiple receptorDragMultiple && args is ArgumentosDragAndDropMultiple argsDragMultiple)
			{
				receptorDragMultiple.OnDragSalio_Impl(argsDragMultiple);
			}
		}

		public virtual bool OnDrop(ArgumentosDragAndDropBase args)
		{
			if (this is IReceptorDeDragUnico receptorDragUnico && args is ArgumentosDragAndDropUnico argsDragUnico)
			{
				return receptorDragUnico.OnDrop_Impl(argsDragUnico);
			}
			else if (this is IReceptorDeDragMultiple receptorDragMultiple && args is ArgumentosDragAndDropMultiple argsDragMultiple)
			{
				return receptorDragMultiple.OnDrop_Impl(argsDragMultiple);
			}

			return false;
		}

	}
}