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

		public new virtual void OnDragEntro(ArgumentosDragAndDropBase args)
		{
			if (args is ArgumentosDragAndDropMultiple argsTipoEspecifico)
				OnDragEntro_Impl(argsTipoEspecifico);
		}

		public new virtual void OnDragSalio(ArgumentosDragAndDropBase args)
		{
			if (args is ArgumentosDragAndDropMultiple argsTipoEspecifico)
				OnDragSalio_Impl(argsTipoEspecifico);
		}

		public new virtual void OnDrop(ArgumentosDragAndDropBase args)
		{
			if (args is ArgumentosDragAndDropMultiple argsTipoEspecifico)
				OnDrop_Impl(argsTipoEspecifico);
		}

		/// <summary>
		/// Metodo que se llamara cuando el cursor entre con un <see cref="Drag"/>
		/// </summary>
		/// <param name="args">Argumentos del evento</param>
		public void OnDragEntro_Impl(ArgumentosDragAndDropMultiple args) { }

		/// <summary>
		/// Metodo que se llamara cuando el cursor salga con un <see cref="Drag"/>
		/// </summary>
		/// <param name="args">Argumentos del evento</param>
		public void OnDragSalio_Impl(ArgumentosDragAndDropMultiple args) { }

		/// <summary>
		/// Metodo que se llamara cuando el usuario suelte el <see cref="Drag"/> sobre este <see cref="IReceptorDeDrag"/>
		/// </summary>
		/// <param name="args">Argumentos del evento</param>
		public bool OnDrop_Impl(ArgumentosDragAndDropMultiple args) => false;
	}
}
