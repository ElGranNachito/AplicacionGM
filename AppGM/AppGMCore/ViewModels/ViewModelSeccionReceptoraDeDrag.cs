using AppGM.Core.Delegados;

namespace AppGM.Core
{
	/// <summary>
	/// <see cref="ViewModel"/> que representa un control cuyo proposito es lidiar con eventos de Drag.
	/// Se utiliza en controles que tengan varios receptores de Drag
	/// </summary>
	public class ViewModelSeccionReceptoraDeDrag : ViewModel, IReceptorDeDrag
	{
		private DDragHandlerElementoSoltado mCallbackElementoSoltado;
		private DDrag mCallBackElementoIngreso;
		private DDrag mCallbackElementoSalio;

		public bool EsVisible { get; set; }

		public int IndiceZ { get; set; }


		public ViewModelSeccionReceptoraDeDrag(DDragHandlerElementoSoltado _callbackElementoSoltado, DDrag _callbackElementoIngreso, DDrag _callbackElementoSalio)
		{
			mCallbackElementoSoltado = _callbackElementoSoltado;
			mCallBackElementoIngreso = _callbackElementoIngreso;
			mCallbackElementoSalio   = _callbackElementoSalio;
		}

		public void OnDragEntro_Impl(IDrageable vm) => mCallBackElementoIngreso?.Invoke(vm);

		public void OnDragSalio_Impl(IDrageable vm) => mCallbackElementoSalio?.Invoke(vm);

		public bool OnDrop_Impl(IDrageable vm)
		{
			return mCallbackElementoSoltado != null
				? mCallbackElementoSoltado(vm)
				: false;
		}
	}
}