using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace AppGM.Core
{
	/// <summary>
	/// 
	/// </summary>
	public class ViewModelSeccionesCondicion : ViewModel
	{
		#region Campos & Propiedades

		//--------------------------------------CAMPOS----------------------------------------------

		/// <summary>
		/// <see cref="ViewModelBloqueCondicionalBase"/> que contiene las secciones
		/// </summary>
		private ViewModelBloqueCondicionalBase mDueño;

		/// <summary>
		/// <see cref="ViewModelCreacionDeFuncionBase"/> que contiene todos los bloques
		/// </summary>
		private ViewModelCreacionDeFuncionBase mVMCreacionDeFuncion;

		/// <summary>
		/// Operaciones realizadas entre los argumentos
		/// </summary>
		public List<EOperacionLogica> operaciones = new List<EOperacionLogica>();

		/// <summary>
		/// Todos los argumentos que vienen despues del <see cref="ArgumentoInicial"/>
		/// </summary>
		public List<ViewModelArgumento> argumentos = new List<ViewModelArgumento>();


		//------------------------------------PROPIEDADES-------------------------------------------

		/// <summary>
		/// Argumento inicial, el que si o si va a estar siempre
		/// </summary>
		public ViewModelArgumento ArgumentoInicial { get; set; }

		/// <summary>
		/// <see cref="ViewModelListaDeElementos"/> que contiene todas las <see cref="ViewModelSeccionCondicion"/>
		/// </summary>
		public ViewModelListaDeElementos<ViewModelSeccionCondicion> Secciones { get; set; } = new ViewModelListaDeElementos<ViewModelSeccionCondicion>();

		/// <summary>
		/// Comando que se ejecuta cuando el usuario presiona el boton para añadir una seccion
		/// </summary>
		public ICommand ComandoAñadirSeccion { get; set; } 

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_vmCreacionDeFuncion"><see cref="ViewModelCreacionDeFuncionBase"/> que contiene todos los bloques</param>
		/// <param name="_dueño"><see cref="ViewModelBloqueCondicionalBase"/> que contiene las secciones</param>
		public ViewModelSeccionesCondicion(
			ViewModelCreacionDeFuncionBase _vmCreacionDeFuncion,
			ViewModelBloqueCondicionalBase _dueño)
		{
			mVMCreacionDeFuncion = _vmCreacionDeFuncion;
			mDueño = _dueño;

			ArgumentoInicial =
				new ViewModelArgumento(mVMCreacionDeFuncion, mDueño, typeof(object), "Argumento0");

			argumentos.Add(ArgumentoInicial);

			ComandoAñadirSeccion = new Comando(AñadirSeccion);
		} 

		#endregion

		#region Metodos

		/// <summary>
		/// Crea una nueva <see cref="ViewModelSeccionCondicion"/> y la añade a <see cref="Secciones"/>
		/// </summary>
		public void AñadirSeccion()
		{
			//Argumento de la seccion anterior a esta
			ViewModelSeccionCondicion argumentoAnterior =
				Secciones.Count != 0 ? Secciones.Elementos.Last() : null;

			//Creamos la nueva seccion y le pasamos el monton de cosas que pide
			ViewModelSeccionCondicion nuevaSeccion = new ViewModelSeccionCondicion(
				argumentoAnterior,
				mVMCreacionDeFuncion,
				mDueño,
				this,
				Secciones.Count + 1,
				argumentoAnterior != null 
					? argumentoAnterior.Argumento.TipoArgumento.ObtenerTipoCompatible() 
					: ArgumentoInicial.TipoArgumento.ObtenerTipoCompatible());

			Secciones.Add(nuevaSeccion);
			argumentos.Add(nuevaSeccion.Argumento);
		}

		/// <summary>
		/// Quita una <paramref name="seccionARemover"/> de <see cref="Secciones"/> y actualiza
		/// los indices de las demas
		/// </summary>
		/// <param name="seccionARemover"><see cref="ViewModelSeccionCondicion"/> que sera removida</param>
		public void QuitarSeccion(ViewModelSeccionCondicion seccionARemover)
		{
			int indiceSeccionAQuitar = Secciones.Elementos.IndexOf(seccionARemover);

			//Si el bloque no esta al final de la lista...
			if (indiceSeccionAQuitar != Secciones.Count - 1)
				//Actualizamos el bloque anterior del bloque siguiente al que sera quitado
				Secciones[indiceSeccionAQuitar + 1].ActualizarArgumentoAnterior(seccionARemover.SeccionAnterior);

			//Finalmente quitamos la seccion
			Secciones.Remove(seccionARemover);
		} 

		#endregion
	}
}