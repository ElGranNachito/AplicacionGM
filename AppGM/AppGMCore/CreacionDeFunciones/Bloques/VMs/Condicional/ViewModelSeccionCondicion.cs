using System;
using System.Linq;
using System.Windows.Input;

using AppGM.Core.Delegados;

namespace AppGM.Core
{
	/// <summary>
	/// Representa una seccion dentro de una condicion compuesta de varias condiciones.
	/// Una seccion esta compuesta por su <see cref="Argumento"/> y <see cref="Operacion"/>
	/// </summary>
	public class ViewModelSeccionCondicion : ViewModel
	{
		#region Campos & Propiedades

		//------------------------------------------CAMPOS----------------------------------------------


		/// <summary>
		/// Funcion que se ejecuta cuando el <see cref="Type"/> de <see cref="SeccionAnterior"/> cambia.
		/// </summary>
		private DVariableCambio<Type> mTipoSeccionAnteriorModificadoHandler;

		/// <summary>
		/// <see cref="ViewModelSeccionesCondicion"/> que contiene a esta seccion
		/// </summary>
		private ViewModelSeccionesCondicion mContenedor;


		//----------------------------------------PROPIEDADES--------------------------------------------


		/// <summary>
		/// <see cref="ViewModelArgumento"/> de esta seccion
		/// </summary>
		public ViewModelArgumento Argumento { get; set; }

		/// <summary>
		/// <see cref="EOperacionLogica"/> que realiza esta seccion
		/// </summary>
		public ViewModelComboBox<EOperacionLogica> Operacion { get; set; }

		/// <summary>
		/// <see cref="ViewModelArgumento"/> que precede a este
		/// </summary>
		public ViewModelSeccionCondicion SeccionAnterior { get; private set; }

		/// <summary>
		/// <see cref="ViewModelArgumento"/> de la <see cref="SeccionAnterior"/>
		/// </summary>
		public ViewModelArgumento ArgumentoSeccionAnterior =>
			SeccionAnterior != null ? SeccionAnterior.Argumento : mContenedor.ArgumentoInicial;

		/// <summary>
		/// Comando que se ejecuta cuando el usuario presiona el boton para eliminar esta seccion
		/// </summary>
		public ICommand ComandoEliminar { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_seccionAnterior"><see cref="ViewModelSeccionCondicion"/> que precede a esta seccion</param>
		/// <param name="_VMCreacionDeFuncionBase"></param>
		/// <param name="_VMContenedor"></param>
		/// <param name="_contenedorSeccion"><see cref="ViewModelSeccionesCondicion"/> que contiene a esta seccion</param>
		/// <param name="_numeroDelArgumento">Indice de esta seccion</param>
		/// <param name="_tipoDelArgumento"><see cref="Type"/> que tendra el <see cref="Argumento"/>. Puede dejarse en null</param>
		public ViewModelSeccionCondicion(
			ViewModelSeccionCondicion _seccionAnterior,
			ViewModelCreacionDeFuncionBase _VMCreacionDeFuncionBase,
			ViewModelBloqueCondicionalBase _VMContenedor,
			ViewModelSeccionesCondicion _contenedorSeccion,
			int _numeroDelArgumento,
			Type _tipoDelArgumento = null)
		{
			mContenedor = _contenedorSeccion;

			Argumento = new ViewModelArgumento(_VMCreacionDeFuncionBase, _VMContenedor,
				_tipoDelArgumento ?? typeof(object), $"Argumento{_numeroDelArgumento}");

			Operacion = new ViewModelComboBox<EOperacionLogica>();
			Operacion.ActualizarValoresPosibles(Enum.GetValues(typeof(EOperacionLogica)).Cast<EOperacionLogica>().ToList());

			ComandoEliminar = new Comando(() => { mContenedor.QuitarSeccion(this); });

			mTipoSeccionAnteriorModificadoHandler = (anterior, actual) =>
			{
				if (anterior.IsAssignableFrom(actual))
					return;

				Argumento.TipoArgumento = actual.ObtenerTipoCompatible();
				Operacion.ActualizarValoresPosibles(actual.ObtenerOperacionesLogicasDisponibles());
			};

			ActualizarArgumentoAnterior(_seccionAnterior);
		}

		#endregion

		#region Metodos

		/// <summary>
		/// Actualiza la <see cref="SeccionAnterior"/> de esta seccion
		/// </summary>
		/// <param name="nuevoIndice">Nuevo indice</param>
		/// <param name="argumentoAnterior">nuevo <see cref="ViewModelArgumento"/> que ahora precede a esta seccion</param>
		public void ActualizarArgumentoAnterior(ViewModelSeccionCondicion argumentoAnterior)
		{
			if (SeccionAnterior == argumentoAnterior)
				return;

			if(ArgumentoSeccionAnterior != null)
				ArgumentoSeccionAnterior.OnTipoArgumentoModificado -= mTipoSeccionAnteriorModificadoHandler;

			SeccionAnterior = argumentoAnterior;

			ArgumentoSeccionAnterior.OnTipoArgumentoModificado += mTipoSeccionAnteriorModificadoHandler;

			Type tipoArgumentoSeccionAnterior = ArgumentoSeccionAnterior.TipoArgumento;

			if (tipoArgumentoSeccionAnterior == typeof(bool))
				return;

			if (tipoArgumentoSeccionAnterior.EsAsignableDesdeOA(Argumento.TipoArgumento))
				return;

			if (SeccionAnterior.SeccionAnterior != null
			    && SeccionAnterior.ArgumentoSeccionAnterior.TipoArgumento.EsAsignableDesdeOA(tipoArgumentoSeccionAnterior))
				return;

			Argumento.TipoArgumento = tipoArgumentoSeccionAnterior.ObtenerTipoCompatible();
		} 

		#endregion
	}
}