namespace AppGM.Core
{
	/// <summary>
	/// Interfaz que describe los metodos necesarios para lidiar con eventos de <see cref="Drag"/> con un unico elemento
	/// </summary>
	public interface IReceptorDeDragUnico : IReceptorDeDrag
	{
		/// <summary>
		/// Metodo que se llamara cuando el cursor entre con un <see cref="Drag"/>
		/// </summary>
		/// <param name="args">Argumentos del evento</param>
		public void OnDragEntro_Impl(ArgumentosDragAndDropUnico args) { }

		/// <summary>
		/// Metodo que se llamara cuando el cursor salga con un <see cref="Drag"/>
		/// </summary>
		/// <param name="args">Argumentos del evento</param>
		public void OnDragSalio_Impl(ArgumentosDragAndDropUnico args) { }

		/// <summary>
		/// Metodo que se llamara cuando el usuario suelte el <see cref="Drag"/> sobre este <see cref="IReceptorDeDrag"/>
		/// </summary>
		/// <param name="args">Argumentos del evento</param>
		public bool OnDrop_Impl(ArgumentosDragAndDropUnico args) => false;
	}
}
