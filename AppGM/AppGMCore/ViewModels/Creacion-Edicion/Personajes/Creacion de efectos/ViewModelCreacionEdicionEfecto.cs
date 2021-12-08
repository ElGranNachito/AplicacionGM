using System;
using System.Threading.Tasks;

namespace AppGM.Core
{
	/// <summary>
	/// Representa un control para la creacion/edicion de un <see cref="ModeloEfecto"/>
	/// </summary>
	public class ViewModelCreacionEdicionEfecto : ViewModelCreacionEdicionDeModelo<ModeloEfecto, ControladorEfecto, ViewModelCreacionEdicionEfecto>
	{
		/// <summary>
		/// 
		/// </summary>
		private ModeloPersonaje mModeloPersonaje;

		private readonly Type mTipoControladorContenedor;

		/// <summary>
		/// Nombre del efecto
		/// </summary>
		public string Nombre
		{
			get => ModeloCreado.Nombre;
			set => ModeloCreado.Nombre = value;
		}

		/// <summary>
		/// Descripcion del efecto
		/// </summary>
		public string Descripcion
		{
			get => ModeloCreado.Descripcion;
			set => ModeloCreado.Descripcion = value;
		}

		/// <summary>
		/// Turnos que dura el efecto
		/// </summary>
		public string TurnosDeDuracion
		{
			get => ModeloCreado.TurnosDeDuracion.ToString();
			set => ModeloCreado.TurnosDeDuracion = int.Parse(value);
		}

		/// <summary>
		/// Indica si este efecto tiene duracion por turnos
		/// </summary>
		public bool EsEfectoConDuracion => ViewModelComboBoxTipoEfecto.Valor == ETipoEfecto.PorTurnos;

		/// <summary>
		/// Viewmodel del combobox para la seleccion del tipo de efecto
		/// </summary>
		public ViewModelComboBox<ETipoEfecto> ViewModelComboBoxTipoEfecto { get; set; } = new ViewModelComboBox<ETipoEfecto>(EnumHelpers.TiposDeEfectoDisponibles);

		/// <summary>
		/// Viewmodel del combobox para la seleccion del comportamiento acumulativo
		/// </summary>
		public ViewModelComboBox<EComportamientoAcumulativo> ViewModelComboBoxComportamientoAcumulativo { get; set; } = new ViewModelComboBox<EComportamientoAcumulativo>(EnumHelpers.ComportamientosAcumulativosDisponibles);

		/// <summary>
		/// Viewmodel del item que representa a la funcion aplicar
		/// </summary>
		public ViewModelListaItems<ViewModelFuncionItem<ControladorFuncion_Efecto>> ViewModelFuncionAplicar { get; set; }

		/// <summary>
		/// Viewmodel del item que representa al predicado puede aplicar
		/// </summary>
		public ViewModelListaItems<ViewModelFuncionItem<ControladorFuncion_PredicadoEfecto>> ViewModelPredicadoPuedeAplicar { get; set; }

		/// <summary>
		/// Lista de items con los eventos disponibles y sus handlers
		/// </summary>
		public ViewModelListaDeElementos<ViewModelCreacionHandlersEvento<TIFuncionHandlerEvento<ModeloEfecto>, ModeloEfecto>> FuncionesHandlerEventos { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_accionSalir">Lambda llamada al salir del control representado por este vm</param>
		/// <param name="_tipoControladorContenedor">Tipo del controlador que contiene a este efecto</param>
		/// <param name="_efectoParaEditar">Controlador del efecto que sera editado</param>
		public ViewModelCreacionEdicionEfecto(
			Action<ViewModelCreacionEdicionEfecto> _accionSalir,
			ModeloPersonaje _modeloPersonaje, 
			Type _tipoControladorContenedor,
			ControladorEfecto _efectoParaEditar = null)

			:base(_accionSalir, _efectoParaEditar)
		{
			mModeloPersonaje = _modeloPersonaje;
			mTipoControladorContenedor = _tipoControladorContenedor;

			CrearComandoEliminar();

			ViewModelComboBoxTipoEfecto.OnValorSeleccionadoCambio += (anterior, actual) =>
			{
				DispararPropertyChanged(nameof(EsEfectoConDuracion));
			};

			ViewModelComboBoxComportamientoAcumulativo.SeleccionarValor(EComportamientoAcumulativo.Solapar);
			ViewModelComboBoxTipoEfecto.SeleccionarValor(ETipoEfecto.Normal);

			ComandoAceptar = new Comando(async () =>
			{
				Resultado = EResultadoViewModel.Aceptar;

				mAccionSalir(this);
			});

			ComandoCancelar = new Comando(() =>
			{
				Resultado = EResultadoViewModel.Cancelar;

				mAccionSalir(this);
			});

			ViewModelFuncionAplicar = new ViewModelListaItems<ViewModelFuncionItem<ControladorFuncion_Efecto>>(async () =>
			{
				var vmCreacion = await new ViewModelCreacionDeFuncionEfecto(
						vm =>
						{
							if (vm.Resultado.EsAceptarOFinalizar())
							{
								var controladorNuevaFuncion = vm.CrearControlador();

								var nuevaFuncion = new TIFuncionEfecto
								{
									Funcion = controladorNuevaFuncion.modelo,
									Efecto = ModeloCreado,

									TipoFuncion = ETipoFuncionEfecto.FuncionAplicar
								};

								AñadirFuncionDesdeListaItems<TIFuncionEfecto, ViewModelFuncionItem<ControladorFuncion_Efecto>>((ViewModelFuncionItem<ControladorFuncion_Efecto>)controladorNuevaFuncion.CrearViewModelItem(), nuevaFuncion, ModeloCreado.Funciones, ViewModelFuncionAplicar);
							}
						}, null).Inicializar();

				SistemaPrincipal.MostrarViewModelCreacionEdicion<ViewModelCreacionDeFuncionBase, ModeloFuncion, ControladorFuncionBase>(vmCreacion);

			}, true, "Funcion aplicar", 1);

			ViewModelPredicadoPuedeAplicar = new ViewModelListaItems<ViewModelFuncionItem<ControladorFuncion_PredicadoEfecto>>(() =>
			{

			}, true, "Predicado", 1);

			
		}

		public override async Task<ViewModelCreacionEdicionEfecto> Inicializar(Type tipoValorPorDefectoModelo = null)
		{
			await base.Inicializar(tipoValorPorDefectoModelo);

			//Obtenemos los eventos disponibles
			var eventosDisponibles = TypeHelpers.ObtenerEventosDisponibles(typeof(ControladorEfecto), new Type[] { mTipoControladorContenedor, typeof(ControladorPersonaje) });

			FuncionesHandlerEventos = new ViewModelListaDeElementos<ViewModelCreacionHandlersEvento<TIFuncionHandlerEvento<ModeloEfecto>, ModeloEfecto>>();

			//Por cada evento...
			foreach (var evento in eventosDisponibles)
			{
				//Harcodeamos un poco
				var nuevoHandlerEvento = new TIFuncionHandlerEvento<ModeloEfecto>
				{
					Otro = ModeloCreado,
					Funcion = new ModeloFuncion_HandlerEvento
					{
						NombreFuncion = "Funcioncita",
						TipoHandlerString = evento.EventHandlerType.AssemblyQualifiedName
					},
					NombresEventosVinculados = string.Empty
				};

				ModeloCreado.HandlersEventos.Add(nuevoHandlerEvento);

				//Añadimos un nuevo item a la lista de eventos y handlers
				FuncionesHandlerEventos.Add(new ViewModelCreacionHandlersEvento<TIFuncionHandlerEvento<ModeloEfecto>, ModeloEfecto>(ModeloCreado.HandlersEventos, evento));
			}

			return this;
		}

		public override ModeloEfecto CrearModelo() => ModeloCreado;

		public override ControladorEfecto CrearControlador()
		{
			ActualizarValidez();

			if (!EsValido)
				return null;

			var modeloEfecto = CrearModelo();

			return new ControladorEfecto(modeloEfecto);
		}

		public override void ActualizarValidez()
		{
			if (ModeloCreado.Nombre.IsNullOrWhiteSpace())
			{
				EsValido = false;

				return;
			}

			if (EsEfectoConDuracion && ModeloCreado.TurnosDeDuracion <= 0)
			{
				EsValido = false;

				return;
			}

			//TODO: Validar funciones

			EsValido = true;
		}
	}
}