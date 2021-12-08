using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AppGM.Core
{
	/// <summary>
	/// Clase que representa un control para la creacion, edicion o vista de un <see cref="ModeloItem"/>
	/// </summary>
	public sealed class ViewModelCreacionEdicionItem : ViewModelCreacionEdicionDeModelo<ModeloItem, ControladorItem, ViewModelCreacionEdicionItem>
	{
		#region Campos & Propiedades

		/// <summary>
		/// Personaje que porta este item
		/// </summary>
		public readonly ModeloPersonaje personajePortador;

		/// <summary>
		/// Controlador del slot que contiene este item
		/// </summary>
		public readonly ControladorSlot controladorSlotContenedor;

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
			get => ModeloCreado.Peso.ToString("F1");
			set
			{
				if (decimal.TryParse(value, out var peso))
					ModeloCreado.Peso = peso;
			}
		}

		/// <summary>
		/// Espacio que ocupa el item
		/// </summary>
		public string EspacioQueOcupa
		{
			get => ModeloCreado.EspacioQueOcupa.ToString("F1");
			set
			{
				if (decimal.TryParse(value, out var peso))
					ModeloCreado.Peso = peso;
			}
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
		public bool EsArma => ViewModelMultiselectComboBoxTipoItem.ItemsSeleccionados.Contains(ETipoItem.Arma);

		/// <summary>
		/// Indica si el tipo de item es un arma a distancia
		/// </summary>
		public bool EsDefensivo => ViewModelMultiselectComboBoxTipoItem.ItemsSeleccionados.Contains(ETipoItem.Defensivo);

		/// <summary>
		/// Indica si el item es un consumible
		/// </summary>
		public bool EsConsumible => ViewModelMultiselectComboBoxTipoItem.ItemsSeleccionados.Contains(ETipoItem.Consumible);

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
		/// Datos del arma
		/// </summary>
		public ViewModelIngresoDatosArma DatosArma { get; set; }

		/// <summary>
		/// Datos de la defensa de este item
		/// </summary>
		public ViewModelIngresoDatosDefensivo DatosDefensivo { get; set; }

		/// <summary>
		/// Datos de consumo
		/// </summary>
		public ViewModelIngresoDatosConsumible DatosConsumible { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_accionSalir">Accion que se ejecuta al salir de este vm</param>
		/// <param name="_personajePortador">Personaje que porta el item</param>
		/// <param name="_controladorParaEditar">Controlador del item que editaremos</param>
		/// <param name="_controladorSlotContenedor">Controlador del slot que contiene el item</param>
		public ViewModelCreacionEdicionItem(
			Action<ViewModelCreacionEdicionItem> _accionSalir,
			ModeloPersonaje _personajePortador,
			ControladorItem _controladorParaEditar = null,
			ControladorSlot _controladorSlotContenedor = null)

			: base(_accionSalir, _controladorParaEditar, true, true)
		{
			personajePortador = _personajePortador;
			controladorSlotContenedor = _controladorSlotContenedor;

			CrearComandoFinalizar();
			CrearComandoEliminar();

			ViewModelMultiselectComboBoxTipoItem = new ViewModelMultiselectComboBox<ETipoItem>(
				EnumHelpers.ObtenerValoresEnum<ETipoItem>(new [] {ETipoItem.TODOS}).Select(t =>
					new ViewModelMultiselectComboBoxItem<ETipoItem>(t, t.ToString(), ViewModelMultiselectComboBoxTipoItem)).ToList(), new FlagsEnumEqualityComparer<ETipoItem>());

			ViewModelComboBoxEstadoPortacion.OnValorSeleccionadoCambio += (anterior, actual) =>
			{
				if(ModeloCreado is null)
					return;

				ModeloCreado.EstadoPortacion = actual.valor;
			};

			ViewModelMultiselectComboBoxTipoItem.OnEstadoSeleccionItemCambio += async item =>
			{
				if (!item.EstaSeleccionado)
				{
					ModeloCreado.TipoItem ^= item.Valor;
				}
				else
				{
					ModeloCreado.TipoItem |= item.Valor;
				}

				switch (item.Valor)
				{
					case ETipoItem.Arma:
					{
						if (item.EstaSeleccionado)
						{
							ModeloCreado.DatosArma ??= new ModeloDatosArma();

							DatosArma = new ViewModelIngresoDatosArma(ModeloCreado.DatosArma, this);
						}
						else
						{
							ModeloCreado.DatosArma.Item = null;

							ModeloCreado.DatosArma = null;
						}

						DispararPropertyChanged(nameof(EsArma));

						break;
					}
					case ETipoItem.Defensivo:
					{
						if (item.EstaSeleccionado)
						{
							ModeloCreado.DatosDefensa ??= new ModeloDatosDefensa();

							DatosDefensivo = new ViewModelIngresoDatosDefensivo(ModeloCreado.DatosDefensa);
						}
						else
						{
							ModeloCreado.DatosDefensa.Item = null;

							ModeloCreado.DatosDefensa = null;
						}

						DispararPropertyChanged(nameof(EsDefensivo));

						break;
					}
					case ETipoItem.Consumible:
					{
						if (item.EstaSeleccionado)
						{
							ModeloCreado.DatosConsumible ??= new ModeloDatosConsumible();

							DatosConsumible = new ViewModelIngresoDatosConsumible(ModeloCreado.DatosConsumible, this);
						}
						else
						{
							ModeloCreado.DatosConsumible.Item = null;
										
							ModeloCreado.DatosConsumible = null;
						}

						DispararPropertyChanged(nameof(EsConsumible));

						break;
					}
				}
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

				SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = new ViewModelCreacionEdicionEfecto(vm =>
				{
					if (vm.Resultado.EsAceptarOFinalizar())
					{
						ModeloCreado.Efectos.Add(vm.CrearModelo());
					}

					SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = dataContextActual;
				}, _personajePortador, typeof(ControladorItem), null);
			}, true, "Efectos");

			//Inicializamos el viewmodel de la lista que contiene las tiradas de este item
			ViewModelListaTiradas = new ViewModelListaItems<ViewModelTiradaItem>(async () =>
			{
				var dataContextActual = SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido;

				SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = await new ViewModelCreacionEdicionDeTirada(vm =>
				{
					if (vm.Resultado.EsAceptarOFinalizar())
					{
						var nuevaTirada = vm.CrearControlador();

						nuevaTirada.modelo.ItemContenedor = ModeloCreado;

						AñadirModeloDesdeListaItems<ModeloTiradaBase, ViewModelTiradaItem>((ViewModelTiradaItem)nuevaTirada.CrearViewModelItem(), ModeloCreado.Tiradas, ViewModelListaTiradas);
					}

					SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = dataContextActual;
				}, ModeloCreado, null).Inicializar();
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

			if (!EstaEditando)
			{
				ModeloCreado.PersonajePortador = personajePortador;

				ViewModelComboBoxEstadoPortacion.SeleccionarValor(EEstadoPortacion.Transportado);

				ViewModelMultiselectComboBoxTipoItem.ModificarEstadoSeleccionItem(ETipoItem.Item, true);
			}
			else
			{
				ViewModelMultiselectComboBoxTipoItem.ModificarEstadoSeleccionItem(ETipoItem.Arma, ModeloCreado.TipoItem.HasFlag(ETipoItem.Arma));
				ViewModelMultiselectComboBoxTipoItem.ModificarEstadoSeleccionItem(ETipoItem.Consumible, ModeloCreado.TipoItem.HasFlag(ETipoItem.Consumible));
				ViewModelMultiselectComboBoxTipoItem.ModificarEstadoSeleccionItem(ETipoItem.Defensivo, ModeloCreado.TipoItem.HasFlag(ETipoItem.Defensivo));

				ViewModelComboBoxEstadoPortacion.SeleccionarValor(ModeloSiendoEditado.EstadoPortacion);

				ViewModelMultiselectComboBoxTipoItem.ModificarEstadoSeleccionItem(ModeloSiendoEditado.TipoItem, true);

				ViewModelListaTiradas.Items.AddRange(ModeloCreado.Tiradas.Select(t => SistemaPrincipal.ObtenerControlador(t, t.ObtenerTipoControlador(), true).CrearViewModelItem() as ViewModelTiradaItem));
				ViewModelListaEfectos.Items.AddRange(ModeloCreado.Efectos.Select(e => SistemaPrincipal.ObtenerControlador(e, e.ObtenerTipoControlador(), true).CrearViewModelItem() as ViewModelEfectoItem));
				ViewModelListaVariables.Items.AddRange(ModeloCreado.Variables.Select(v => SistemaPrincipal.ObtenerControlador(v, v.ObtenerTipoControlador(), true).CrearViewModelItem() as ViewModelVariableItem));
			}

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
			ActualizarValidez();

			return EsValido ? ModeloCreado : null;
		}

		public override ControladorItem CrearControlador()
		{
			var modelo = CrearModelo();

			return modelo == null ? null : SistemaPrincipal.ObtenerControlador<ControladorItem, ModeloItem>(modelo, true);
		}

		public override void ActualizarValidez()
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

			if (controladorSlotContenedor != null)
			{
				if (EstaEditando)
				{
					if(controladorSlotContenedor.EspacioDisponible < ModeloCreado.Peso - ModeloSiendoEditado.Peso)
						return;
				}
				else
				{
					if(controladorSlotContenedor.EspacioDisponible < ModeloCreado.EspacioQueOcupa)
						return;
				}
			}

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

			//Si el item es un arma nos aseguramos de que los datos del arma sean validos
			if(ViewModelMultiselectComboBoxTipoItem.ItemsSeleccionados.Contains(ETipoItem.Arma) && (DatosArma is null || !DatosArma.EsValido))
				return;

			//Si el item es un consumible nos aseguramos de que los datos del consumible sea valido
			if (ViewModelMultiselectComboBoxTipoItem.ItemsSeleccionados.Contains(ETipoItem.Consumible) && (DatosConsumible is null || !DatosConsumible.EsValido))
				return;

			//Si el item es defensivo nos aseguramos de que los datos de defensa sean validos
			if (ViewModelMultiselectComboBoxTipoItem.ItemsSeleccionados.Contains(ETipoItem.Defensivo) && (DatosDefensivo is null || !DatosDefensivo.EsValido))
				return;

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
