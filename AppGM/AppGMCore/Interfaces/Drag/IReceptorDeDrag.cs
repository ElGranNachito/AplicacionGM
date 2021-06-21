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
		/// <see cref="OnDragEnter"/>, <see cref="OnDragLeave"/>, <see cref="OnDrop"/>
		/// </summary>
		public int IndiceZ { get; set; }

		public virtual void OnDragEntro(IDrageable vm) => OnDragEntro_Impl(vm);

		public virtual void OnDragSalio(IDrageable vm) => OnDragSalio_Impl(vm);

		public virtual bool OnDrop(IDrageable vm) => OnDrop_Impl(vm);

		/// <summary>
		/// Funcion que se llamara cuando el cursor entre con un <see cref="Drag"/>
		/// </summary>
		public void OnDragEntro_Impl(IDrageable vm);

		/// <summary>
		/// Funcion que se llamara cuando el cursor salga con un <see cref="Drag"/>
		/// </summary>
		public void OnDragSalio_Impl(IDrageable vm);

		/// <summary>
		/// Funcion que se llamara cuando el usuario suelte el <see cref="Drag"/> sobre este <see cref="IReceptorDeDrag"/>
		/// </summary>
		public bool OnDrop_Impl(IDrageable vm);
	}
}