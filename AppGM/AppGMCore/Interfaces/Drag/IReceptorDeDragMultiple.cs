namespace AppGM.Core
{
	/// <summary>
	/// Interfaz que describe los metodos necesarios para lidiar con eventos de <see cref="Drag"/> con multiples elementos
	/// </summary>
	public interface IReceptorDeDragMultiple : IReceptorDeDrag
	{
		/// <summary>
		/// Elemento que contiene a todos los <see cref="IReceptorDeDragMultiple"/> que participan del drag and drop
		/// </summary>
		public IHostDragAndDropMultiple HostDragAndDrop { get; set; }

		/// <summary>
		/// Metodo que se llamara cuando el cursor entre con un <see cref="Drag"/>
		/// </summary>
		/// <param name="args">Argumentos del evento</param>
		public virtual void OnDragEntro_Impl(ArgumentosDragAndDropMultiple args) { }

		/// <summary>
		/// Metodo que se llamara cuando el cursor salga con un <see cref="Drag"/>
		/// </summary>
		/// <param name="args">Argumentos del evento</param>
		public virtual void OnDragSalio_Impl(ArgumentosDragAndDropMultiple args) { }

		/// <summary>
		/// Metodo que se llamara cuando el usuario suelte el <see cref="Drag"/> sobre este <see cref="IReceptorDeDrag"/>
		/// </summary>
		/// <param name="args">Argumentos del evento</param>
		public virtual bool OnDrop_Impl(ArgumentosDragAndDropMultiple args) => false;
	}
}
