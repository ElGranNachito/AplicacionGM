using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoolLogs;

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


		//--------------------------PROPIEDADES------------------------------

		/// <summary>
		/// Resultado de <see cref="CrearFuncion"/>
		/// </summary>
		public ResultadoCompilacion<TFuncion> ResultadoCompilacion { get; private set; }

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
						(ControladorFuncion<TFuncion>)ControladorFuncionBase.CrearControladorCorrespondiente(modeloFuncion, PropositoFuncion);
				}

				//Actualizamos el nombre de la funcion en el modelo
				mControladorFuncion.NombreFuncion = NombreFuncion;

				return mControladorFuncion;
			}

			set => mControladorFuncion = value;
		}

		public ICommand ComandoCompilar { get; set; }

		public ICommand ComandoGuardar { get; set; }

		public ICommand ComandoCancelar { get; set; }

		public ICommand ComandoAceptar { get; set; }

		#endregion

		#region Constructor

		protected ViewModelCreacionDeFuncion(Action<ViewModelCreacionDeFuncionBase> accionSalir, ControladorFuncion<TFuncion> _controladorFuncion, EPropositoFuncion _propositoDeFuncion)
		{
			PropositoFuncion = _propositoDeFuncion;

			ComandoCompilar = new Comando(async () =>
			{
				PuedeCompilar = false;
				PuedeGuardar  = false;

				MostrarContenedorFelicitaciones = await CrearFuncion();

				MostrarContenedorFelicitaciones = false;
				PuedeCompilar = true;
			});

			ComandoGuardar  = new Comando(async () =>
			{
				PuedeGuardar  = false;
				PuedeCompilar = false;

				await ControladorFuncion.ActualizarBloquesAsync(VariablesBase.Concat(
					from bloque in Bloques
					select bloque.GenerarBloque()).ToList());

				PuedeGuardar  = true;
				PuedeCompilar = true;
			});

			ComandoCancelar = new Comando(() =>
			{
				SistemaPrincipal.Desatar<ViewModelCreacionDeFuncionBase>();

				Resultado = EResultadoViewModel.Cancelar;

				accionSalir(this);
			});

			ComandoAceptar = new Comando(() =>
			{
				SistemaPrincipal.Desatar<ViewModelCreacionDeFuncionBase>();

				Resultado = EResultadoViewModel.Aceptar;

				accionSalir(this);
			});

			//No queremos disparar la propiedad si el controlador es null asi que hacemos este if
			if (_controladorFuncion != null)
			{
				ControladorFuncion = _controladorFuncion;

				NombreFuncion = ControladorFuncion.NombreFuncion;

				DebeCargarBloquesDesdeControlador = true;
			}
		}

		#endregion

		#region Metodos

		public override async void CargarBloquesFuncion()
		{
			if (!DebeCargarBloquesDesdeControlador)
				return;

			DebeCargarBloquesDesdeControlador = false;

			var resultado = await ControladorFuncion.CargarBloquesAsync();

			if (resultado)
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

		/// <summary>
		/// Funcion llamada por <see cref="ComandoCompilar"/>
		/// </summary>
		//Este metodo no es abstracto porque no se puede tener un metodo async sin cuerpo
		protected async Task<bool> CrearFuncion()
		{
			foreach (var bloque in Bloques)
			{
				if (!bloque.VerificarValidez())
					await Task.FromResult(false);
			}

			var bloques = VariablesBase.Concat(
				from bloque in Bloques
				select bloque.GenerarBloque()).ToList();

			ResultadoCompilacion = await Task.Run(() =>
			{
				SistemaPrincipal.ThreadUISyncContext.Post(s => { Logs.Add(new ViewModelLog("Actualizando bloques...")); }, null);

				ControladorFuncion.ActualizarBloques(bloques);

				SistemaPrincipal.ThreadUISyncContext.Post(s =>
				{
					PuedeGuardar = true;

					Logs.Add(new ViewModelLog("Iniciando compilacion..."));
				}, null);

				Compilador compilador = new Compilador(bloques);

				return compilador
					.Compilar<TFuncion>();
			});

			if (ResultadoCompilacion.FueExitosa)
			{
				Logs.Add(new ViewModelLog("Compilacion finalizada con exito!"));

				var controladorHabilidad = new ControladorHabilidad(new ModeloHabilidad { Nombre = "Ultra destructor de nada" });

				ControladorPersonaje[] objectivos = new ControladorPersonaje[1];
			}
			else
				Logs.Add(new ViewModelLog($"Compilacion fallo! {ResultadoCompilacion.Mensaje}", ESeveridad.Error));

			return ResultadoCompilacion.FueExitosa;
		}

		#endregion
	}
}