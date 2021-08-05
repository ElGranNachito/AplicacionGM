using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace AppGM.Core
{
	/// <summary>
	/// <see cref="ViewModel"/> que representa los parametros de una funcion
	/// </summary>
	public class ViewModelBloqueArgumentosFuncion : ViewModelBloqueFuncionBase
	{
		#region Campos & Propiedades

		/// <summary>
		/// Funcion
		/// </summary>
		private MetodoAccesibleEnGuraScratch mMetodo;

		/// <summary>
		/// Bloque que contiene a estos argumentos
		/// </summary>
		private ViewModelBloqueFuncionBase mBloqueContenedor;

		/// <summary>
		/// Lista de <see cref="ViewModelArgumento"/> que corresponden a cada parametro
		/// </summary>
		public ObservableCollection<ViewModelArgumento> ArgumentosFuncion { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_vmCrecionDeFuncion"></param>
		/// <param name="_funcion"><see cref="MethodInfo"/> para el que se ingresaran los parametros</param>
		public ViewModelBloqueArgumentosFuncion(
			ViewModelBloqueFuncionBase _bloqueContenedor,
			MetodoAccesibleEnGuraScratch _funcion,
			List<ParametrosInicializarArgumentoDesdeBloque> argumentosFuncion = null)

			: base(_bloqueContenedor.VMCreacionDeFuncion, _bloqueContenedor.IDBloque)
		{
			if (argumentosFuncion == null)
			{
				ArgumentosFuncion = new ObservableCollection<ViewModelArgumento>();

				ActualizarFuncion(_funcion);
			}
			else
			{
				mMetodo = _funcion;

				var argsFunc = argumentosFuncion.Select(args =>
				{
					args.contenedor = this;
					args.idBloque   = IDBloque;

					return new ViewModelArgumento(args);
				});

				ArgumentosFuncion = new ObservableCollection<ViewModelArgumento>(argsFunc);
			}

			mBloqueContenedor = _bloqueContenedor;
		}

		#endregion

		#region Metodos

		public override List<BloqueVariable> ObtenerVariables() => mBloqueContenedor?.ObtenerVariables();

		/// <summary>
		/// Genera los respectivos <see cref="BloqueArgumento"/> para cada argumento de la funcion
		/// </summary>
		/// <returns></returns>
		public List<BloqueArgumento> ObtenerArgumentosFuncion() => ArgumentosFuncion.Select(arg => arg.GenerarBloque_Impl()).ToList();

		/// <summary>
		/// Actualiza la <see cref="mMetodo"/> para la que estamos ingresando argumentos
		/// </summary>
		/// <param name="funcion"><see cref="MethodInfo"/> nueva funcion</param>
		public void ActualizarFuncion(MetodoAccesibleEnGuraScratch funcion)
		{
			if (funcion == mMetodo)
				return;
			
			mMetodo = funcion;

			ArgumentosFuncion.Clear();

			for (int i = 0; i < mMetodo.Parametros.Length; ++i)
			{
				ArgumentosFuncion.Add(
					new ViewModelArgumento(
						this,
						mMetodo.Parametros[i].ParameterType,
						mMetodo.ObtenerNombreParametro(i),
						mMetodo.Parametros[i].ParameterType == typeof(object), 
						mMetodo.Parametros[i].IsOptional));
			}
		}

		public override bool VerificarValidez()
		{
			ParameterInfo[] parametros = mMetodo.Parametros;

			//Si la cantidad de parametros requeridos no es igual a la cantidad
			//provista entonces retornamos false
			if (parametros.Length != ArgumentosFuncion.Count)
				return false;

			for (int i = 0; i < parametros.Length; ++i)
			{
				ArgumentosFuncion[i].ActualizarValidez();

				if (!ArgumentosFuncion[i].EsValido)
					return false;

				//No deberia poder ocurrir pero por si acaso revisamos que se pueda asignar al parametro
				//utilizando el argumento que le corresponde
				if (!parametros[i].ParameterType.IsAssignableFrom(ArgumentosFuncion[i].TipoArgumento))
					return false;
			}

			return true;
		}

		public bool EsValidoPara(MethodInfo metodo) => mMetodo.Metodo == metodo;

		#endregion
	}
}