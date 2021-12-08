namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel base para los <see cref="ViewModelElementoArbol{TContenido}"/> que contiene aquellos metodos y propiedades
	/// independiente del tipo de elmento contenido
	/// </summary>
	public abstract class ViewModelElementoArbolBase : ViewModel, IDrageableMultiple, IReceptorDeDragMultiple
	{
		public IHostDragAndDropMultiple HostDragAndDrop { get; set; }

		/// <summary>
		/// Contiene el valor de <see cref="EstaSeleccionado"/>
		/// </summary>
		protected bool mEstaSeleccionado;

		/// <summary>
		/// Contiene el valor de <see cref="EstaExpandido"/>
		/// </summary>
		protected bool mEstaExpandido;

		/// <summary>
		/// Indica si este elemento se encuentra seleccionado
		/// </summary>
		public virtual bool EstaSeleccionado { get; set; }

		/// <summary>
		/// Indica si este elemento se encuentra espandido actualmente
		/// </summary>
		public virtual bool EstaExpandido { get; set; }

		/// <summary>
		/// Indica si este elemento es visible
		/// </summary>
		public virtual bool EsVisible { get; set; } = true;

		public virtual int IndiceZ { get; set; }

		public abstract void RemoverDePadre();


		public virtual void OnComienzoDrag_Impl(ArgumentosDragAndDropMultiple args)
		{

		}

		public virtual void OnDragEntro_Impl(ArgumentosDragAndDropMultiple args)
		{

		}

		public virtual void OnDragSalio_Impl(ArgumentosDragAndDropMultiple args)
		{

		}

		public virtual bool OnDrop_Impl(ArgumentosDragAndDropMultiple args)
		{
			return false;
		}

		/// <summary>
		/// Indica si este elemento puede ser seleccionado
		/// </summary>
		/// <returns><see cref="bool"/> indicando si este elemento puede ser seleccionado</returns>
		public virtual bool PuedeSerSeleccionado() => true;

		/// <summary>
		/// Actualiza los <see cref="Hijos"/> de este elemento
		/// </summary>
		public virtual void ActualizarHijos() { }

		/// <summary>
		/// Realiza una actualizacion general del elemento
		/// </summary>
		public virtual void Actualizar() => ActualizarHijos();

		public virtual bool PuedeSerDragueado() => EstaSeleccionado;
	}
}
