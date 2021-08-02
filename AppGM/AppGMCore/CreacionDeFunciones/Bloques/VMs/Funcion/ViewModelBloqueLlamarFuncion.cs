using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel que representa un <see cref="BloqueFuncion"/>
	/// </summary>
	public class ViewModelBloqueLlamarFuncion : ViewModelBloqueFuncion<BloqueFuncion>
	{
		#region Campos & Propiedades

		//-----------------------------------------CAMPOS------------------------------------------

		/// <summary>
		/// Indica si se deberia actualizar <see cref="MetodoSeleccionado"/>
		/// </summary>
		private bool mDeberiaActualizarMetodos;

		/// <summary>
		/// Almacena el valor de <see cref="MetodoSeleccionado"/>
		/// </summary>
		private MetodoAccesibleEnGuraScratch mMetodoSeleccionado;


		//--------------------------------------PROPIEDADES-----------------------------------------

		/// <summary>
		/// Indica si mostrar la lista de funciones disponibles y los argumentos
		/// </summary>
		public bool MostrarListaMetodosDisponibles { get; set; } = true;

		/// <summary>
		/// Argumento desde el que se llama a la funcion
		/// </summary>
		public ViewModelArgumento Caller { get; set; }

		/// <summary>
		/// Argumentos de la funcion
		/// </summary>
		public ViewModelBloqueArgumentosFuncion ArgumentosFuncion => mMetodoSeleccionado?.ArgumentosFuncion;

		/// <summary>
		/// Metodos disponibles para seleccionar
		/// </summary>
		public ViewModelListaDeElementos<ViewModelItemComboBoxBase<MethodInfo>> MetodosDisponibles { get; set; } = new ViewModelListaDeElementos<ViewModelItemComboBoxBase<MethodInfo>>();

		/// <summary>
		/// Elemento de <see cref="MetodosDisponibles"/> actualmente seleccionado
		/// </summary>
		public ViewModelItemComboBoxBase<MethodInfo> MetodoSeleccionado
		{
			get
			{
				if (mMetodoSeleccionado == null)
					return null;

				return new ViewModelItemComboBoxBase<MethodInfo>(mMetodoSeleccionado.Metodo, mMetodoSeleccionado.ObtenerNombre());
			}
			set
			{
				if (value != null)
				{
					if (mMetodoSeleccionado == null)
						mMetodoSeleccionado = new MetodoAccesibleEnGuraScratch(this, value.valor);
					else
						mMetodoSeleccionado.Actualizar(value.valor);

					DispararPropertyChanged(new PropertyChangedEventArgs(nameof(ArgumentosFuncion)));
				}
				else
					mMetodoSeleccionado = null;
			}
		}

		#endregion

		#region Constructores

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_vmCreacionFuncion"><see cref="ViewModelCreacionDeFuncionBase"/> al que pertenece este bloque</param>
		public ViewModelBloqueLlamarFuncion(ViewModelCreacionDeFuncionBase _vmCreacionFuncion)
			: base(_vmCreacionFuncion)
		{
			Caller = new ViewModelArgumento(this, typeof(object), "", true, false);

			Caller.OnFocusPerdido += ActualizarListaMetodos;
			Caller.OnTipoArgumentoModificado += (anterior, actual) => mDeberiaActualizarMetodos = true;
		}

		/// <summary>
		/// Inicializa este ViewModel a partir de datos preexistentes
		/// </summary>
		/// <param name="_idBloque"></param>
		/// <param name="_vmCreacionFuncion"><see cref="ViewModelCreacionDeFuncionBase"/> al que pertenece este bloque</param>
		/// <param name="_bloque">Bloque que esta creando este ViewModel</param>
		/// <param name="_paramsCaller">Parametros con los que se inicializara al <see cref="Caller"/></param>
		public ViewModelBloqueLlamarFuncion(
			int _idBloque,
			ViewModelCreacionDeFuncionBase _vmCreacionFuncion,
			BloqueFuncion _bloque,
			ParametrosInicializarArgumentoDesdeBloque _paramsCaller)

			:base(_vmCreacionFuncion, _idBloque)
		{
			_paramsCaller.contenedor = this;

			Caller = new ViewModelArgumento(_paramsCaller);

			Caller.OnFocusPerdido += ActualizarListaMetodos;
			Caller.OnTipoArgumentoModificado += (anterior, actual) => mDeberiaActualizarMetodos = true;

			//Actualizamos la lista de metodos disponibles
			mDeberiaActualizarMetodos = true;
			
			ActualizarListaMetodos();

			//Hacemos que el metodo seleccionado sea el que nos pasaron
			mMetodoSeleccionado = _bloque.ObtenerMetodoAccesibleEnGuraScratch(this);

			//Le avisamos a la UI que el metodo actualmente seleccionado cambio
			DispararPropertyChanged(new PropertyChangedEventArgs(nameof(MetodoSeleccionado)));
		}

		#endregion

		public override BloqueFuncion GenerarBloque_Impl()
		{
			return mMetodoSeleccionado.GenerarBloque(Caller.GenerarBloque_Impl());
		}

		/// <summary>
		/// Actualiza <see cref="MetodosDisponibles"/> en base al <see cref="ValorSeleccionado"/>
		/// </summary>
		private void ActualizarListaMetodos()
		{
			if (!mDeberiaActualizarMetodos)
				return;

			mDeberiaActualizarMetodos = false;

			MetodosDisponibles.Elementos.Clear();

			MetodosDisponibles.AddRange(Caller.TipoArgumento.ObtenerMetodosAccesiblesEnGuraScratch().Select(
				metodo => new ViewModelItemComboBoxBase<MethodInfo>(metodo.metodo, metodo.nombre)));
		}

		public override bool VerificarValidez()
		{
			return mMetodoSeleccionado != null &&
			       Caller.EsValido &&
			       mMetodoSeleccionado.VerificarValidez();
		}
	}
}