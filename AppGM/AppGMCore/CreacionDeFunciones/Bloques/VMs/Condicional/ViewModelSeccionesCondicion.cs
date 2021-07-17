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
		/// <see cref="ViewModelBloqueCondicional"/> que contiene las secciones
		/// </summary>
		private ViewModelBloqueCondicional mDueño;

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
		/// <param name="_dueño"><see cref="ViewModelBloqueCondicional"/> que contiene las secciones</param>
		public ViewModelSeccionesCondicion(
			ViewModelCreacionDeFuncionBase _vmCreacionDeFuncion,
			ViewModelBloqueCondicional _dueño)
		{
			mVMCreacionDeFuncion = _vmCreacionDeFuncion;
			mDueño = _dueño;

			AñadirSeccion();

			ArgumentoInicial = Secciones.Last().Argumento;

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
				Secciones.Count);

			nuevaSeccion.Argumento.OnFocusPerdido += ActualizarValidezDeLasSecciones;

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

			for (int i = indiceSeccionAQuitar + 1; i < Secciones.Count; ++i)
				Secciones[i].Argumento.Nombre = $"Argumento{i}";

			seccionARemover.Argumento.OnFocusPerdido -= ActualizarValidezDeLasSecciones;

			//Finalmente quitamos la seccion
			Secciones.Remove(seccionARemover);
		}

		/// <summary>
		/// Actualiza la propiedad <see cref="ViewModelSeccionCondicion.EsValida"/> de todas las <see cref="Secciones"/>
		/// </summary>
		public void ActualizarValidezDeLasSecciones()
		{
			for (int i = 0; i < Secciones.Count;)
			{
				var seccionActual = Secciones[i];

				//Si el tipo del argumento de la seccion actual es booleano entonces no 'acompaña' a ninguna otra seccion
				if (seccionActual.Argumento.TipoArgumento == typeof(bool))
				{
					++i;

					//La seccion actual es valida si el argumento es valido
					seccionActual.EsValida = seccionActual.Argumento.EsValido;
				}
				//Si es cualquier otra cosa entonces va a necesitar un 'compañero' de un tipo compatible para realizar la operacion logica
				else
				{
					//Si esta es la ultima seccion entonces simplemente no es valida
					if (i == Secciones.Count - 1)
					{
						Secciones[i].EsValida = false;

						break;
					}

					var seccionProxima = Secciones[i + 1];

					//Revisamos que las secciones sean compatibles entre si, para esto una debe ser asignable a la otra
					bool sonCompatiblesEntreSi = seccionActual.Argumento.TipoArgumento.EsAsignableDesdeOA(seccionProxima.Argumento.TipoArgumento);

					//Las secciones seran validas si son compatibles entre si y sus respectivos argumentos validos
					seccionActual.EsValida  = sonCompatiblesEntreSi && seccionActual.Argumento.EsValido;
					seccionProxima.EsValida = sonCompatiblesEntreSi && seccionProxima.Argumento.EsValido;

					//Avanzamos dos posiciones porque cubrimos dos secciones
					i += 2;
				}
			}
		}

		#endregion
	}
}