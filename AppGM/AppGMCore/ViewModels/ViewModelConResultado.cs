
using System;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel que produce un resultado cuando el usuario sale del control que representa
	/// </summary>
	public abstract class ViewModelConResultado<T> : ViewModel
		where T: ViewModelConResultado<T>
	{
		/// <summary>
		/// Representa la accion a llevar a cabo cuando se sale de este vm
		/// </summary>
		protected Action<T> mAccionSalir;

		/// <summary>
		/// Resultado del vm
		/// </summary>
		public EResultadoViewModel Resultado { get; protected set; }

		public ViewModelConResultado(Action<T> _accionSalir)
		{
			mAccionSalir = _accionSalir;
		}

		public ViewModelConResultado(){}
	}
}
