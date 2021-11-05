using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppGM.Core
{
	/// <summary>
	/// Clase que representa un control para la creacion, edicion o vista de un <see cref="ModeloItem"/>
	/// </summary>
	public class ViewModelCreacionEdicionItem : ViewModelCreacionEdicionDeModelo<ModeloItem, ControladorItem, ViewModelCreacionEdicionItem>
	{
		#region Propiedades

		/// <summary>
		/// Nombre del item
		/// </summary>
		public string Nombre
		{
			get => ModeloCreado.Nombre;
			set => ModeloCreado.Nombre = value;
		}

		/// <summary>
		/// Descripcion del item
		/// </summary>
		public string Descripcion
		{
			get => ModeloCreado.Descripcion;
			set => ModeloCreado.Descripcion = value;
		}

		/// <summary>
		/// Peso del item
		/// </summary>
		public string Peso
		{
			get => ModeloCreado.Peso.ToString("##.####");
			set => ModeloCreado.Peso = decimal.Parse(value);
		}

		/// <summary>
		/// Espacio que ocupa el item
		/// </summary>
		public string EspacioQueOcupa
		{
			get => ModeloCreado.EspacioQueOcupa.ToString("##.####");
			set => ModeloCreado.EspacioQueOcupa = decimal.Parse(value);
		}

		/// <summary>
		/// Estado % del item
		/// </summary>
		public string EstadoPorcentual
		{
			get => ModeloCreado.Estado.ToString();
			set => ModeloCreado.Estado = int.Parse(value);
		}

		/// <summary>
		/// Indica si el tipo de item es un arma a distancia
		/// </summary>
		public bool EsArmaADistancia => ViewModelMultiselectComboBoxTipoItem.ItemsSeleccionados.Contains(ETipoItem.ArmaDistancia);

		/// <summary>
		/// Viewmodel que representa a la combobox de seleccion de <see cref="EEstadoPortacion"/>
		/// </summary>
		public ViewModelComboBox<EEstadoPortacion> ViewModelComboBoxEstadoPortacion { get; set; } = new ViewModelComboBox<EEstadoPortacion>(EnumHelpers.EstadosDePortacionDisponibles);

		/// <summary>
		/// Viewmodel que representa a la combobox de seleccion de <see cref="ETipoItem"/>
		/// </summary>
		public ViewModelMultiselectComboBox<ETipoItem> ViewModelMultiselectComboBoxTipoItem { get; set; }

		/// <summary>
		/// Funcion utilizar
		/// </summary>
		public ViewModelListaItems<ViewModelFuncionItem<ControladorFuncion_Efecto>> ViewModelFuncionUtilizar { get; set; }

		/// <summary>
		/// Predicado puede utilizar
		/// </summary>
		public ViewModelListaItems<ViewModelFuncionItem<ControladorFuncion_PredicadoItem>> ViewModelFuncionPuedeUtilizar { get; set; }

		/// <summary>
		/// Lista de efectos del item
		/// </summary>
		public ViewModelListaItems<ViewModelEfectoItem> ViewModelListaEfectos { get; set; }

		/// <summary>
		/// Lista de tiradas que tiene el item
		/// </summary>
		public ViewModelListaItems<ViewModelTiradaItem> ViewModelListaTiradas { get; set; }

		/// <summary>
		/// Lista de variables que tiene el item
		/// </summary>
		public ViewModelListaItems<ViewModelVariableItem> ViewModelListaVariables { get; set; }

		/// <summary>
		/// Handlers de eventos
		/// </summary>
		public ViewModelListaDeElementos<ViewModelCreacionHandlersEvento<TIFuncionHandlerEvento<ModeloItem>, ModeloItem>> ViewModelListaHandlersEventos { get; set; }

		/// <summary>
		/// Controlador del slot que contiene este item
		/// </summary>
		public ControladorSlot ControladorSlotContenedor { get; init; }

		#endregion

		#region Constructor

		public ViewModelCreacionEdicionItem(Action<ViewModelCreacionEdicionItem> _accionSalir, ModeloPersonaje _personajeContenedor, ControladorItem _controladorParaEditar = null, ControladorSlot _controladorSlotContenedor = null)
			: base(_accionSalir, _controladorParaEditar)
		{
			ControladorSlotContenedor = _controladorSlotContenedor;

			CrearComandoFinalizar();
			CrearComandoEliminar();

			ViewModelMultiselectComboBoxTipoItem = new ViewModelMultiselectComboBox<ETipoItem>(
				EnumHelpers.TiposItemDisponibles.Select(t =>
					new ViewModelMultiselectComboBoxItem<ETipoItem>(t, t.ToString(), ViewModelMultiselectComboBoxTipoItem)).ToList(), new FlagsEnumEqualityComparer<ETipoItem>());

			if (!EstaEditando)
			{
				ViewModelComboBoxEstadoPortacion.SeleccionarValor(EEstadoPortacion.Transportado);

				ViewModelMultiselectComboBoxTipoItem.ModificarEstadoSeleccionItem(ETipoItem.Item, true);
			}
			else
			{
				ViewModelComboBoxEstadoPortacion.SeleccionarValor(ModeloSiendoEditado.EstadoPortacion);

				ViewModelMultiselectComboBoxTipoItem.ModificarEstadoSeleccionItem(ModeloSiendoEditado.TipoItem, true);
			}

			ViewModelMultiselectComboBoxTipoItem.OnEstadoSeleccionItemCambio += item =>
			{
				DispararPropertyChanged(nameof(EsArmaADistancia));
			};

			//Inicializamos la lista para crear la funcion de utilizacion
			ViewModelFuncionUtilizar = new ViewModelListaItems<ViewModelFuncionItem<ControladorFuncion_Efecto>>(() =>
			{
				var dataContextActual = SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido;

				SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = new ViewModelCreacionDeFuncionItem(vm =>
				{
					if (vm.Resultado.EsAceptarOFinalizar())
					{
						var controladorFuncion = vm.CrearControlador();

						var nuevaRelacion = new TIFuncionItem
						{
							Funcion = controladorFuncion.modelo,
							Item = ModeloCreado,

							PropositoFuncionRelacion = EPropositoFuncionRelacion.Uso
						};

						ModeloCreado.Funciones.Add(nuevaRelacion);
					}

					SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = dataContextActual;
				}, null);
			}, true, "Funcion utilizar", 1);

			//Inicializamos la lista para crear el predicado de puede utilizar
			ViewModelFuncionPuedeUtilizar = new ViewModelListaItems<ViewModelFuncionItem<ControladorFuncion_PredicadoItem>>(() =>
			{
				var dataContextActual = SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido;

				SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = new ViewModelCreacionDeFuncionPredicadoItem(vm =>
				{
					if (vm.Resultado.EsAceptarOFinalizar())
					{
						var controladorFuncion = vm.CrearControlador();

						var nuevaRelacion = new TIFuncionItem
						{
							Funcion = controladorFuncion.modelo,
							Item = ModeloCreado,

							PropositoFuncionRelacion = EPropositoFuncionRelacion.PredicadoUso
						};

						ModeloCreado.Funciones.Add(nuevaRelacion);
					}

					SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = dataContextActual;

				}, null);
			}, true, "Funcion puede utilizar", 1);

			//Inicializamos el viewmodel de la lista que contiene los efectos de este item
			ViewModelListaEfectos = new ViewModelListaItems<ViewModelEfectoItem>(() =>
			{
				var dataContextActual = SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido;

				SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = new ViewModelCreacionEfecto(vm =>
				{
					if (vm.Resultado.EsAceptarOFinalizar())
					{
						ModeloCreado.Efectos.Add(vm.CrearModelo());
					}

					SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = dataContextActual;
				}, _personajeContenedor, typeof(ControladorItem), null);
			}, true, "Efectos");

			//Inicializamos el viewmodel de la lista que contiene las tiradas de este item
			ViewModelListaTiradas = new ViewModelListaItems<ViewModelTiradaItem>(() =>
			{
				var dataContextActual = SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido;

				SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = new ViewModelCreacionEdicionDeTirada(vm =>
				{
					if (vm.Resultado.EsAceptarOFinalizar())
					{
						ModeloCreado.Tiradas.Add(vm.CrearModelo());
					}

					SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = dataContextActual;
				}, ModeloCreado, null);
			}, true, "Tiradas");

			//Inicializamos el viewmodel de la lista de variables de este item
			ViewModelListaVariables = new ViewModelListaItems<ViewModelVariableItem>(() =>
			{
				var dataContextActual = SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido;

				SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = new ViewModelCreacionDeVariable(vm =>
				{
					if (vm.Resultado.EsAceptarOFinalizar())
					{
						ModeloCreado.Variables.Add(vm.CrearModelo());
					}

					SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = dataContextActual;
				});
			}, true, "Variables");
		}

		#endregion

		#region Metodos

		public override async Task<ViewModelCreacionEdicionItem> Inicializar(Type tipoValorPorDefectoModelo = null)
		{
			await base.Inicializar(tipoValorPorDefectoModelo);

			var eventosDisponibles = TypeHelpers.ObtenerEventosDisponibles(typeof(ControladorItem), new[] { typeof(ControladorPersonaje) });

			ViewModelListaHandlersEventos = new ViewModelListaDeElementos<ViewModelCreacionHandlersEvento<TIFuncionHandlerEvento<ModeloItem>, ModeloItem>>();

			//Por cada evento...
			foreach (var evento in eventosDisponibles)
			{
				//Harcodeamos un poco
				var nuevoHandlerEvento = new TIFuncionHandlerEvento<ModeloItem>
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
				ViewModelListaHandlersEventos.Add(new ViewModelCreacionHandlersEvento<TIFuncionHandlerEvento<ModeloItem>, ModeloItem>(ModeloCreado.HandlersEventos, evento));
			}

			return this;
		}

		public override ModeloItem CrearModelo()
		{
			throw new NotImplementedException();
		}

		public override ControladorItem CrearControlador()
		{
			throw new NotImplementedException();
		}

		protected override void ActualizarValidez()
		{
			EsValido = false;

			//Nos aseguramos de que el item tenga un nombre
			if (Nombre.IsNullOrWhiteSpace()) 
				return;

			//Nos aseguramos de que el peso del item no sea inferior a cero
			if(ModeloCreado.Peso < 0)
				return;

			//Nos aseguramos de que el espacio ocupado por el item no sea inferior a cero
			if(ModeloCreado.EspacioQueOcupa < 0)
				return;

			//Nos aseguramos de que el estado del item este en un rango valido
			if(ModeloCreado.Estado is < 0 or > 100)
				return;

			//Nos aseguramos de que si la funcion de utilizar existe, sea valida
			if (ViewModelFuncionUtilizar.Items.Count > 0 && !ViewModelFuncionUtilizar.Items[0].ControladorGenerico.Modelo.EsValido)
				return;

			//Nos aseguramos de que si existe el predicado de puede utilizar, sea valido
			if (ViewModelFuncionPuedeUtilizar.Items.Count > 0 && !ViewModelFuncionPuedeUtilizar.Items[0].ControladorGenerico.Modelo.EsValido)
				return;

			//Nos aseguramos de que todos los efectos creados sean validos
			foreach (var efecto in ViewModelListaEfectos.Items)
			{
				if(!efecto.ControladorGenerico.modelo.EsValido)
					return;
			}

			EsValido = true;
		}

		public override string ToString()
		{
			var prefijo = ObtenerPrefijoTituloVentana();

			if (EstaEditando)
				return $"{prefijo} - {ModeloCreado.Nombre}";

			return $"{prefijo} - {ModeloCreado.TipoItem}";
		} 

		#endregion
	}
}
