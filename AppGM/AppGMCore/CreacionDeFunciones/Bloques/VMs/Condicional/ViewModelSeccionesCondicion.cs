using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace AppGM.Core
{
	/// <summary>
	/// Contiene todos los <see cref="ViewModelSeccionCondicion"/> y <see cref="EOperacionLogica"/> que se realizan
	/// en un <see cref="ViewModelBloqueCondicional"/>
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
		public List<EOperacionLogica> operaciones => Secciones.Skip(1).Select(s => s.Operacion.Valor).ToList();

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

		#region Constructores

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_dueño"><see cref="ViewModelBloqueCondicional"/> que contiene las secciones</param>
		public ViewModelSeccionesCondicion(
			ViewModelBloqueCondicional _dueño)
		{
			mVMCreacionDeFuncion = _dueño.VMCreacionDeFuncion;
			mDueño = _dueño;

			AñadirSeccion();

			ArgumentoInicial = Secciones.Last().Argumento;

			ComandoAñadirSeccion = new Comando(()=>{AñadirSeccion();});
		}

		/// <summary>
		/// Constructor que se utiliza al cargar secciones ya existentes
		/// </summary>
		/// <param name="_dueño"><see cref="ViewModelBloqueCondicional"/> que contiene las secciones</param>
		/// <param name="_argumentos"><see cref="List{T}"/> con los <see cref="ParametrosInicializarArgumentoDesdeBloque"/> que se utilizaran para inicializar los <see cref="argumentos"/></param>
		/// <param name="_operacionesLogicas"><see cref="List{T}"/> con las <see cref="EOperacionLogica"/> que se realizaran entre los <see cref="argumentos"/></param>
		public ViewModelSeccionesCondicion(
			ViewModelBloqueCondicional _dueño,
			List<ParametrosInicializarArgumentoDesdeBloque> _argumentos,
			List<EOperacionLogica> _operacionesLogicas)
		{
			//Añadimos la primera seccion a mano ya que esta no lleva una operacion logica
			AñadirSeccion(new ViewModelSeccionCondicion(mDueño, this, 0, _argumentos[0].tipoArgumento, _argumentos[0]));

			//Añadimos el resto de las secciones
			for (int i = 1; i < _argumentos.Count; ++i)
			{
				AñadirSeccion(new ViewModelSeccionCondicion(mDueño, this, i, _argumentos[i].tipoArgumento, _argumentos[i], _operacionesLogicas[i - 1]));
			}

			ArgumentoInicial = Secciones.Last().Argumento;

			ComandoAñadirSeccion = new Comando(() => { AñadirSeccion(); });

			ActualizarValidezDeLasSecciones();
		}

		#endregion

		#region Metodos

		/// <summary>
		/// Crea una nueva <see cref="ViewModelSeccionCondicion"/> y la añade a <see cref="Secciones"/>
		/// </summary>
		public void AñadirSeccion(ViewModelSeccionCondicion nuevaSeccion = null)
		{
			//Argumento de la seccion anterior a esta
			ViewModelSeccionCondicion argumentoAnterior =
				Secciones.Count != 0 ? Secciones.Elementos.Last() : null;

			//Creamos la nueva seccion y le pasamos el monton de cosas que pide
			nuevaSeccion ??= new ViewModelSeccionCondicion(
				mDueño,
				this,
				Secciones.Count);

			nuevaSeccion.Argumento.OnFocusPerdido += ActualizarValidezDeLasSecciones;

			Secciones.Add(nuevaSeccion);
			argumentos.Add(nuevaSeccion.Argumento);

			nuevaSeccion.Inicializar(argumentoAnterior);
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
					bool sonCompatiblesEntreSi = seccionActual.Argumento.TipoArgumento.EsComparableA(seccionProxima.Argumento.TipoArgumento);

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