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

		public virtual void OnDragEntro(ArgumentosDragAndDropBase args) {}

		public virtual void OnDragSalio(ArgumentosDragAndDropBase args) {}

		public virtual bool OnDrop(ArgumentosDragAndDropBase args) => false;

	}
}