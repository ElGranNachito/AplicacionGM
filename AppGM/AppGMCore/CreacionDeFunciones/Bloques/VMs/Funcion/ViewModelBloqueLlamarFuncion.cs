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

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_vmCreacionFuncion"><see cref="ViewModelCreacionDeFuncionBase"/> al que pertenece este bloque</param>
		public ViewModelBloqueLlamarFuncion(ViewModelCreacionDeFuncionBase _vmCreacionFuncion)
			: base(_vmCreacionFuncion)
		{
			Caller = new ViewModelArgumento(this, typeof(object), "", true, false);

			Caller.OnFocusPerdido += ActualizarListaFunciones;
			Caller.OnTipoArgumentoModificado += (anterior, actual) => mDeberiaActualizarMetodos = true;
		} 

		#endregion

		public override BloqueFuncion GenerarBloque_Impl()
		{
			return mMetodoSeleccionado.GenerarBloque(Caller.GenerarBloque_Impl());
		}

		/// <summary>
		/// Actualiza <see cref="MetodosDisponibles"/> en base al <see cref="ValorSeleccionado"/>
		/// </summary>
		private void ActualizarListaFunciones()
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

		//private void ActualizarValoresDisponibles()
		//{
		//	ValoresDisponibles.Elementos.Clear();

		//	IEnumerable<ViewModelItemComboBoxBase<object>> variablesDisponibles = new List<ViewModelItemComboBoxBase<object>>();

		//	variablesDisponibles = VMCreacionDeFuncion.VariablesBase.Select(var => new ViewModelItemComboBoxBase<object>(var, var.nombre));

		//	variablesDisponibles = variablesDisponibles.Concat(mPadre.ObtenerVariablesCreadas(this)
		//		.Select(var =>
		//		{
		//			var nuevoItem = new ViewModelItemComboBoxBase<object>();

		//			nuevoItem.Actualizar(var, var.Nombre, "", "", (sender, args) =>
		//			{
		//				if (sender == var && args.PropertyName == nameof(ViewModelBloqueDeclaracionVariable.Nombre))
		//					nuevoItem.Texto = var.Nombre;
		//			});

		//			return nuevoItem;
		//		}));

		//	ValoresDisponibles.AddRange(variablesDisponibles.Concat(TiposDisponibles));
		//}

		//private void AñadirVariableAPosibilidades(ViewModelBloqueFuncionBase bloque, IContenedorDeBloques padre)
		//{
		//	if (bloque is ViewModelBloqueDeclaracionVariable vmVar)
		//	{
		//		var var = vmVar.GenerarBloque_Impl();

		//		ValoresDisponibles.Add(new ViewModelItemComboBoxBase<object> {Texto = var.nombre, valor = var});
		//	}
		//}

		//private void QuitarVariableDePosibilidades(ViewModelBloqueFuncionBase bloque, IContenedorDeBloques padre)
		//{
		//	if (bloque is ViewModelBloqueDeclaracionVariable vmVar)
		//	{
		//		var var = vmVar.GenerarBloque_Impl();

		//		ValoresDisponibles.RemoveFirst(elemento => elemento.valor == var);
		//	}
		//}

		//public override void OnBloqueRemovido()
		//{
		//	VMCreacionDeFuncion.OnBloqueAñadido  -= AñadirVariableAPosibilidades;
		//	VMCreacionDeFuncion.OnBloqueRemovido -= QuitarVariableDePosibilidades;
		//}
	}
}