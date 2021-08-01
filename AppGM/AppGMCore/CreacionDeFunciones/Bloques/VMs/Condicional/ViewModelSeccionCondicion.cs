using System;
using System.ComponentModel;
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

		/// <summary>
		/// Almacena el valor de <see cref="IndiceSeccion"/>
		/// </summary>
		private int mIndiceSeccion = -1;

		//----------------------------------------PROPIEDADES--------------------------------------------

		/// <summary>
		/// Indica si esta seccion es valida
		/// </summary>
		public bool EsValida { get; set; }

		/// <summary>
		/// Indica si debe mostrar la combo box para seleccionar la operacion logica
		/// </summary>
		public bool MostrarOperacionLogica => IndiceSeccion > 0;

		/// <summary>
		/// Indice de esta seccion
		/// </summary>
		public int IndiceSeccion
		{
			get => mIndiceSeccion;
			set
			{
				if (value == mIndiceSeccion)
					return;

				mIndiceSeccion = value;

				Argumento.Nombre = $"Argumento{mIndiceSeccion}";

				if(mIndiceSeccion == 0)
					DispararPropertyChanged(new PropertyChangedEventArgs(nameof(MostrarOperacionLogica)));
			}
		}

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
		public ViewModelArgumento ArgumentoSeccionAnterior => SeccionAnterior?.Argumento;

		/// <summary>
		/// Grosor del borde del contenedor
		/// </summary>
		public Grosor GrosorBorde => EsValida ? new Grosor(0) : new Grosor(0, 0, 0, 2);

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
		/// <param name="_VMContenedor"></param>
		/// <param name="_contenedorSeccion"><see cref="ViewModelSeccionesCondicion"/> que contiene a esta seccion</param>
		/// <param name="_indiceSeccion">Indice de esta seccion</param>
		/// <param name="_tipoDelArgumento"><see cref="Type"/> que tendra el <see cref="Argumento"/>. Puede dejarse en null</param>
		public ViewModelSeccionCondicion(
			ViewModelBloqueCondicional _VMContenedor,
			ViewModelSeccionesCondicion _contenedorSeccion,
			int _indiceSeccion,
			Type _tipoDelArgumento = null)
		{
			mContenedor   = _contenedorSeccion;

			Argumento = new ViewModelArgumento(_VMContenedor,
				_tipoDelArgumento ?? typeof(object), $"Argumento{_indiceSeccion}", true, false);

			IndiceSeccion = _indiceSeccion;

			Operacion = new ViewModelComboBox<EOperacionLogica>();
			Operacion.ActualizarValoresPosibles(Argumento.TipoArgumento.ObtenerOperacionesLogicasDisponibles());

			ComandoEliminar = new Comando(() => { mContenedor.QuitarSeccion(this); });
			
			mTipoSeccionAnteriorModificadoHandler = (anterior, actual) =>
			{
				//Si se puede asignar al nuevo tipo desde el tipo de esta seccion o el nuevo tipo es booleano entonces simplemente salimos de la funcion.
				if (actual.EsAsignableDesdeOA(Argumento.TipoArgumento) || actual == typeof(bool))
					return;

				ActualizarTipoArgumento();
			};
		}

		#endregion

		#region Metodos

		public void Inicializar(ViewModelSeccionCondicion _seccionAnterior)
		{
			ActualizarArgumentoAnterior(_seccionAnterior);
		}

		/// <summary>
		/// Actualiza la <see cref="SeccionAnterior"/> de esta seccion
		/// </summary>
		/// <param name="argumentoAnterior">nuevo <see cref="ViewModelArgumento"/> que ahora precede a esta seccion</param>
		public void ActualizarArgumentoAnterior(ViewModelSeccionCondicion argumentoAnterior)
		{
			//Si la nueva seccion anterior es la misma que la actual entonces no hacemos nada
			if (SeccionAnterior == argumentoAnterior)
				return;

			if(ArgumentoSeccionAnterior != null)
				ArgumentoSeccionAnterior.OnTipoArgumentoModificado -= mTipoSeccionAnteriorModificadoHandler;

			SeccionAnterior = argumentoAnterior;

			ArgumentoSeccionAnterior.OnTipoArgumentoModificado += mTipoSeccionAnteriorModificadoHandler;

			ActualizarTipoArgumento();
		}

		/// <summary>
		/// Actualiza el <see cref="Type"/> del <see cref="Argumento"/> en base al <see cref="ArgumentoSeccionAnterior"/>
		/// </summary>
		private void ActualizarTipoArgumento()
		{
			int indiceActual = 0;
			int indiceSeccionActual = IndiceSeccion;

			bool necesitaUnTipoCompatibleConLaSeccionAnterior = false;

			while (true)
			{
				//Si el indice actual es igual o superior al indice de esta seccion entonces salimos del bucle
				if (indiceActual >= indiceSeccionActual)
					break;

				//Si el tipo del argumento de la seccion actual es booleano entonces no 'acompaña' a ninguna otra seccion
				if (mContenedor.Secciones.Elementos[indiceActual].Argumento.TipoArgumento == typeof(bool))
				{
					++indiceActual;
				}
				//Si es cualquier otra cosa entonces va a necesitar un 'compañero' de un tipo compatible para realizar la operacion logica
				else if (mContenedor.Secciones.Elementos[indiceActual].Argumento.TipoArgumento.EsComparableA(mContenedor.Secciones.Elementos[indiceActual + 1].Argumento.TipoArgumento))
				{
					//Aumentamos el indice en dos porque lidiamos con dos secciones en este caso
					indiceActual += 2;
				}
				//Si no se dio ninguno de los casos anteriores y la proxima seccion es esta entonces deberemos actualizar su tipo
				else if (indiceActual + 1 == indiceSeccionActual)
				{
					necesitaUnTipoCompatibleConLaSeccionAnterior = true;

					break;
				}
			}

			if (necesitaUnTipoCompatibleConLaSeccionAnterior)
			{
				Argumento.TipoArgumento = ArgumentoSeccionAnterior.TipoArgumento.ObtenerTipoCompatible();

				Operacion.ActualizarValoresPosibles(Argumento.TipoArgumento.ObtenerOperacionesLogicasDisponibles());
			}
		}

		#endregion
	}
}