
using System;
using System.Windows.Input;

namespace AppGM.Core
{
	public abstract class ViewModelConResultadoBase : ViewModel
	{
		public Action<EResultadoViewModel> OnResultadoEstablecido = delegate { };

		private EResultadoViewModel mResultado = EResultadoViewModel.NoEstablecido;

		/// <summary>
		/// Resultado del vm
		/// </summary>
		public EResultadoViewModel Resultado
		{
			get => mResultado;
			protected set
			{
				//Si el resultado ya ha sido establecido entonces regresamos
				if (Resultado != EResultadoViewModel.NoEstablecido)
					return;

				mResultado = value;

				OnResultadoEstablecido(mResultado);
			}
		}
	}

	/// <summary>
	/// Viewmodel que produce un resultado cuando el usuario sale del control que representa
	/// </summary>
	public abstract class ViewModelConResultado<T> : ViewModelConResultadoBase
		where T: ViewModelConResultado<T>
	{
		/// <summary>
		/// Representa la accion a llevar a cabo cuando se sale de este vm
		/// </summary>
		protected Action<T> mAccionSalir;

		/// <summary>
		/// Comando que se ejecuta al presionar 'Confirmar'
		/// </summary>
		public ICommand ComandoAceptar { get; protected set; }

		/// <summary>
		/// Comando que se ejecuta al presionar 'Cancelar'
		/// </summary>
		public ICommand ComandoCancelar { get; protected set; }

		public ViewModelConResultado(Action<T> _accionSalir)
		{
			mAccionSalir = _accionSalir;
		}

		public ViewModelConResultado(){}
	}
}
