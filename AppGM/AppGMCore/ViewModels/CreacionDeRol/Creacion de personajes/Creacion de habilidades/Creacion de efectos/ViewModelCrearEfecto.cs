using System;
using System.Windows.Input;

namespace AppGM.Core
{
	/// <summary>
	/// Representa un control para la creacion/edicion de un <see cref="ModeloEfecto"/>
	/// </summary>
	public class ViewModelCrearEfecto : ViewModelConResultado<ViewModelCrearEfecto>
	{
		/// <summary>
		/// Modelo del efecto 
		/// </summary>
		public ModeloEfecto Efecto { get; private set; }

		/// <summary>
		/// Controlador del <see cref="ModeloEfecto"/> que esta siendo editado
		/// </summary>
		public ControladorEfecto EfectoSiendoEditado { get; private set; }

		/// <summary>
		/// Comando que se ejecuta al presionar 'Confirmar'
		/// </summary>
		public ICommand ComandoConfirmar { get; private set; }

		/// <summary>
		/// Comando que se ejecuta al presionar 'Cancelar'
		/// </summary>
		public ICommand ComandoCancelar { get; private set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_accionSalir">Lambda llamada al salir del control representado por este vm</param>
		/// <param name="_efectoParaEditar">Controlador del efecto que sera editado</param>
		public ViewModelCrearEfecto(Action<ViewModelCrearEfecto> _accionSalir, ControladorEfecto _efectoParaEditar = null)
			:base(_accionSalir)
		{
			EfectoSiendoEditado = _efectoParaEditar;

			if (EfectoSiendoEditado != null)
				Efecto = EfectoSiendoEditado.modelo.Clonar() as ModeloEfecto;
			else
				Efecto = new ModeloEfecto();

			ComandoConfirmar = new Comando(() =>
			{
				EfectoSiendoEditado?.ActulizarModelo(Efecto);

				Resultado = EResultadoViewModel.Aceptar;

				mAccionSalir(this);
			});

			ComandoCancelar = new Comando(() =>
			{
				Resultado = EResultadoViewModel.Cancelar;

				mAccionSalir(this);
			});
		}
	}
}