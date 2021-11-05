using System.Linq;
using System.Windows.Input;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel que representa a un control para la vista de fuentes de daño
	/// </summary>
	public class ViewModelVistaFuentesDeDaño : ViewModel
	{
		#region Campos & Propiedades

		//-------------------------------CAMPOS----------------------------------

		/// <summary>
		/// Contiene el valor de <see cref="FuenteDeDañoSeleccionada"/>
		/// </summary>
		private ViewModelFuenteDeDañoItem mFuenteDeDañoSeleccionada;

		/// <summary>
		/// Modelo del rol que contiene estas fuentes de daño
		/// </summary>
		public readonly ModeloRol modeloRol;

		//----------------------------PROPIEDADES--------------------------------

		/// <summary>
		/// Indica si el control de creacion/edicion de fuente de daño debe ser visible
		/// </summary>
		public bool MostrarControlCreacionEdicionFuenteDeDaño => ViewModelCreacionEdicionFuenteDeDaño != null;

		/// <summary>
		/// Viewmodel del control de creacion/edicion de la fuente de daño seleccionada
		/// </summary>
		public ViewModelCreacionEdicionFuenteDeDaño ViewModelCreacionEdicionFuenteDeDaño { get; set; }

		/// <summary>
		/// Fuentes de daño existentes
		/// </summary>
		public ViewModelListaDeElementos<ViewModelFuenteDeDañoItem> FuentesDeDaño { get; set; } = new ViewModelListaDeElementos<ViewModelFuenteDeDañoItem>();

		/// <summary>
		/// Fuente de daño actualmente seleccionada
		/// </summary>
		public ViewModelFuenteDeDañoItem FuenteDeDañoSeleccionada
		{
			get => mFuenteDeDañoSeleccionada;
			set
			{
				if (value == mFuenteDeDañoSeleccionada)
					return;

				SetFuenteDeDañoSeleccionada(value);
			}
		}

		/// <summary>
		/// Comando que se ejecuta cuando el usuario presiona el boton 'Añadir'
		/// </summary>
		public ICommand ComandoAñadirNueva { get; private set; }

		#endregion

		#region Constructor

		public ViewModelVistaFuentesDeDaño(ModeloRol _modeloRol)
		{
			modeloRol = _modeloRol;

			FuentesDeDaño.AddRange(modeloRol.FuentesDeDaño.Select(f => new ViewModelFuenteDeDañoItem(f)));

			ComandoAñadirNueva = new Comando(async () =>
			{
				ViewModelCreacionEdicionFuenteDeDaño = await new ViewModelCreacionEdicionFuenteDeDaño(vm =>
			   {
				   if (vm.Resultado.EsAceptarOFinalizar())
				   {
					   var nuevaFuenteDeDaño = vm.CrearModelo();

					   FuentesDeDaño.Add(new ViewModelFuenteDeDañoItem(nuevaFuenteDeDaño));

					   modeloRol.FuentesDeDaño.Add(nuevaFuenteDeDaño);

					   SetFuenteDeDañoSeleccionada(FuentesDeDaño.Last());
				   }
				   else if (vm.Resultado == EResultadoViewModel.Eliminar)
				   {
					   FuentesDeDaño.Remove(mFuenteDeDañoSeleccionada);

					   modeloRol.FuentesDeDaño.Remove(mFuenteDeDañoSeleccionada.fuenteDeDaño);
				   }
				   else
				   {
					   ViewModelCreacionEdicionFuenteDeDaño = null;

					   DispararPropertyChanged(nameof(MostrarControlCreacionEdicionFuenteDeDaño));
				   }
			   }, null).Inicializar();
			});
		}

		#endregion

		#region Metodos

		/// <summary>
		/// Establece el valor de <see cref="mFuenteDeDañoSeleccionada"/>
		/// </summary>
		/// <param name="nuevoValor">Item que ahora esta seleccionado</param>
		private async void SetFuenteDeDañoSeleccionada(ViewModelFuenteDeDañoItem nuevoValor)
		{
			mFuenteDeDañoSeleccionada = nuevoValor;

			if (mFuenteDeDañoSeleccionada == null)
			{
				ViewModelCreacionEdicionFuenteDeDaño = null;

				DispararPropertyChanged(nameof(MostrarControlCreacionEdicionFuenteDeDaño));

				return;
			}

			ViewModelCreacionEdicionFuenteDeDaño = await new ViewModelCreacionEdicionFuenteDeDaño(async vm =>
			{
				if (vm.Resultado.EsAceptarOFinalizar())
				{
					await vm.CrearModelo().CrearCopiaProfundaEnSubtipoAsync<ModeloFuenteDeDaño, ModeloFuenteDeDaño>(mFuenteDeDañoSeleccionada.fuenteDeDaño);
				}
				else if (vm.Resultado == EResultadoViewModel.Eliminar)
				{
					FuentesDeDaño.Remove(mFuenteDeDañoSeleccionada);
				}
				else
				{
					FuenteDeDañoSeleccionada = null;
				}

			}, mFuenteDeDañoSeleccionada.fuenteDeDaño).Inicializar();
		} 

		#endregion
	}
}