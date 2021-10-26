namespace AppGM.Core
{
	/// <summary>
	/// Interfaz que describe los metodos necesarios para lidiar con eventos de <see cref="Drag"/> con un unico elemento
	/// </summary>
	public interface IReceptorDeDragUnico : IReceptorDeDrag
	{
		public new virtual void OnDragEntro(ArgumentosDragAndDropBase args)
		{
			if(args is ArgumentosDragAndDropUnico argsTipoEspecifico)
				OnDragEntro_Impl(argsTipoEspecifico);
		}

		public new virtual void OnDragSalio(ArgumentosDragAndDropBase args)
		{
			if (args is ArgumentosDragAndDropUnico argsTipoEspecifico)
				OnDragSalio_Impl(argsTipoEspecifico);
		}

		public new virtual void OnDrop(ArgumentosDragAndDropBase args)
		{
			if (args is ArgumentosDragAndDropUnico argsTipoEspecifico)
				OnDrop_Impl(argsTipoEspecifico);
		}

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
