using AppGM.Core.Delegados;

namespace AppGM.Core
{
	/// <summary>
	/// <see cref="ViewModel"/> que representa un control cuyo proposito es lidiar con eventos de Drag.
	/// Se utiliza en controles que tengan varios receptores de Drag
	/// </summary>
	public class ViewModelSeccionReceptoraDeDrag<TArgs> : ViewModel, IReceptorDeDrag

		where TArgs : ArgumentosDragAndDropBase
	{
		private DDragHandlerElementoSoltado<TArgs> mCallbackElementoSoltado;
		private DDrag<TArgs> mCallBackElementoIngreso;
		private DDrag<TArgs> mCallbackElementoSalio;

		public bool EsVisible { get; set; }

		public int IndiceZ { get; set; }


		public ViewModelSeccionReceptoraDeDrag(DDragHandlerElementoSoltado<TArgs> _callbackElementoSoltado, DDrag<TArgs> _callbackElementoIngreso, DDrag<TArgs> _callbackElementoSalio)
		{
			mCallbackElementoSoltado = _callbackElementoSoltado;
			mCallBackElementoIngreso = _callbackElementoIngreso;
			mCallbackElementoSalio   = _callbackElementoSalio;
		}

		public void OnDragEntro(ArgumentosDragAndDropBase args)
		{
			if(args is TArgs argsTipoEspecifico)
				mCallBackElementoIngreso?.Invoke(argsTipoEspecifico);
		}

		public void OnDragSalio(ArgumentosDragAndDropBase args)
		{
			if (args is TArgs argsTipoEspecifico)
				mCallbackElementoSalio?.Invoke(argsTipoEspecifico);
		}

		public bool OnDrop(ArgumentosDragAndDropBase args)
		{
			if (args is TArgs argsTipoEspecifico)
				return mCallbackElementoSoltado?.Invoke(argsTipoEspecifico) ?? false;

			return false;
		}
	}
}