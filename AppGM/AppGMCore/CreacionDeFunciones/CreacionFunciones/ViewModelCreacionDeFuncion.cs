using System.Linq;
using System.Windows.Input;

namespace AppGM.Core
{
	/// <summary>
	/// View model base para la creacion de una funcion, ya sea para una habilidad, efecto, condicion.
	/// </summary>
	/// <typeparam name="TFuncion">Tipo de la funcion que sera creada</typeparam>
	public abstract class ViewModelCreacionDeFuncion<TFuncion> : ViewModelCreacionDeFuncionBase
	{

		#region Propiedad & Campos

		//-----------------------------CAMPOS--------------------------------

		private ControladorFuncion<TFuncion> mControladorFuncion;

		/// <summary>
		/// Resultado de <see cref="CrearFuncion"/>
		/// </summary>
		private ResultadoCompilacion<TFuncion> mResultadoCompilacion;

		//--------------------------PROPIEDADES------------------------------

		/// <summary>
		/// Controlador de la funcion actualmente siendo creada
		/// </summary>
		public ControladorFuncion<TFuncion> ControladorFuncion
		{
			get
			{
				if (mControladorFuncion == null)
				{
					ModeloFuncion modeloFuncion = new ModeloFuncion();

					mControladorFuncion =
						(ControladorFuncion<TFuncion>)ControladorFuncionBase.CrearControladorCorrespondiente(modeloFuncion, TipoFuncion);
				}

				//Actualizamos el nombre de la funcion en el modelo
				mControladorFuncion.NombreFuncion = NombreFuncion;

				return mControladorFuncion;
			}

			set => mControladorFuncion = value;
		}

		public ICommand ComandoCompilar { get; set; }

		public ICommand ComandoCancelar { get; set; }

		public ICommand ComandoGuardar { get; set; } 

		#endregion

		#region Constructor

		public ViewModelCreacionDeFuncion(ControladorFuncion<TFuncion> _controladorFuncion, ETipoFuncion _tipoDeFuncion)
		{
			TipoFuncion = _tipoDeFuncion;

			ComandoCompilar = new Comando(CrearFuncion);
			ComandoGuardar = new Comando(Guardar);

			ComandoCancelar = new Comando(() =>
			{
				//TODO: Expandir para que tambien cambie el contenido actual de la ventana o dispare algun evento para que se haga en otro lado
				SistemaPrincipal.Desatar<ViewModelCreacionDeFuncionBase>();
			});

			//No queremos disparar la propiedad si el controlador es null asi que hacemos este if
			if (_controladorFuncion != null)
			{
				ControladorFuncion = _controladorFuncion;

				NombreFuncion = ControladorFuncion.NombreFuncion;

				if (ControladorFuncion.CargarBloques())
				{
					var bloques = ControladorFuncion.Bloques.FindAll(bloque =>
					{
						if (bloque is BloqueVariable var)
						{
							if (var.Argumento == null)
								return false;

							return true;
						}

						return true;
					}).Select(bloque => bloque.ObtenerViewModel(this));

					foreach (var bloque in bloques)
					{
						AñadirBloque(bloque, -1);
					}
				}
			}
		} 

		#endregion

		#region Metodos

		/// <summary>
		/// Funcion llamada por <see cref="ComandoCompilar"/>
		/// </summary>
		protected abstract void CrearFuncion();

		/// <summary>
		/// Funcion llamada por <see cref="ComandoGuardar"/>
		/// </summary>
		protected abstract void Guardar(); 

		#endregion
	}
}