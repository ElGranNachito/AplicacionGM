
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
	public abstract class ViewModelConResultado<TViewModel> : ViewModelConResultadoBase
		where TViewModel : ViewModelConResultado<TViewModel>
	{
		/// <summary>
		/// Representa la accion a llevar a cabo cuando se sale de este vm
		/// </summary>
		protected readonly Action<TViewModel> mAccionSalir;

		/// <summary>
		/// Comando que se ejecuta al presionar 'Confirmar'
		/// </summary>
		public ICommand ComandoAceptar { get; protected set; }

		/// <summary>
		/// Comando que se ejecuta al presionar 'Cancelar'
		/// </summary>
		public ICommand ComandoCancelar { get; protected set; }

		#region Constructores

		/// <summary>
		/// Constructor que inicializa este vm con una <paramref name="_accionSalir"/>
		/// </summary>
		/// <param name="_accionSalir">Accion que se ejecuta al salir de este vm</param>
		/// <param name="crearComandosPorDefecto">Indica si debe crear los comandos <see cref="ComandoAceptar"/> y <see cref="ComandoCancelar"/></param>
		public ViewModelConResultado(Action<TViewModel> _accionSalir, bool crearComandosPorDefecto = true)
		{
			mAccionSalir = _accionSalir;

			if (crearComandosPorDefecto)
				CrearComandos();
		}

		/// <summary>
		/// Constructor por defecto
		/// </summary>
		/// <param name="crearComandosPorDefecto">Indica si debe crear los comandos <see cref="ComandoAceptar"/> y <see cref="ComandoCancelar"/></param>
		public ViewModelConResultado(bool crearComandosPorDefecto = true)
		{
			if (crearComandosPorDefecto)
				CrearComandos();
		} 

		#endregion

		#region Metodos

		private void CrearComandos()
		{
			ComandoAceptar = new Comando(() =>
			{
				Resultado = EResultadoViewModel.Aceptar;

				mAccionSalir?.Invoke((TViewModel)this);
			});

			ComandoCancelar = new Comando(() =>
			{
				Resultado = EResultadoViewModel.Cancelar;

				mAccionSalir?.Invoke((TViewModel)this);
			});
		} 

		#endregion
	}
}
