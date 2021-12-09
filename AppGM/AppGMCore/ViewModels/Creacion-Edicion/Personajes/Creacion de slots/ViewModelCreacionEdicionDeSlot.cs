using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel que representa un control para la creacion o edicion de un <see cref="ModeloSlot"/>
	/// </summary>
	public sealed class ViewModelCreacionEdicionDeSlot : ViewModelCreacionEdicionDeModelo<ModeloSlot, ControladorSlot, ViewModelCreacionEdicionDeSlot>
	{
		#region Eventos

		/// <summary>
		/// Evento que se dispara cuando se realiza alguna modificacion al slot o su contenido
		/// </summary>
		public event Action<ControladorSlot> OnSlotModificado = delegate { }; 

		#endregion

		#region Campos & Propiedades

		/// <summary>
		/// Delegado que contiene el metodo que se ejecuta cuando se sale del <see cref="ViewModelCreacionEdicionItem"/>
		/// </summary>
		private Action<ViewModelCreacionEdicionItem> mAccionSalirCreacionEdicionItem;

		/// <summary>
		/// Nombre del slot
		/// </summary>
		public string NombreSlot
		{
			get => ModeloCreado.NombreSlot;
			set
			{
				if(ModeloCreado.NombreSlot == value)
					return;

				ModeloCreado.NombreSlot = value;

				OnSlotModificado(ControladorSiendoEditado);
			}
		}

		/// <summary>
		/// Espacio total del slot
		/// </summary>
		public string EspacioSlot
		{
			get => ModeloCreado.EspacioTotal.ToString("##.###");
			set
			{
				var nuevoEspacioOcupado = ConversionHelpers.ParseToDecimalIfValid(value);

				if(nuevoEspacioOcupado == decimal.MinValue)
					return;

				ModeloCreado.EspacioTotal = nuevoEspacioOcupado;

				OnSlotModificado(ControladorSiendoEditado);
			}
		}

		/// <summary>
		/// Indica si debemos mostrar el menu para editar el slot
		/// </summary>
		public bool MostrarSlot { get; set; }

		/// <summary>
		/// Indica si debemos mostrar el contenido del slot
		/// </summary>
		public bool MostrarContenido { get; set; }

		/// <summary>
		/// Indica si se debe mostrar el menu de creacion de contenido
		/// </summary>
		public bool MostrarMenuCrearContenido => ModeloCreado.ParteDelCuerpoAlmacenada == null && ModeloCreado.ItemsAlmacenados.Count == 0;

		/// <summary>
		/// Indica si se debe mostrar la lista de items
		/// </summary>
		public bool MostrarListaItems => ModeloCreado.ParteDelCuerpoAlmacenada == null;

		/// <summary>
		/// Viewmodel para el control de creacion/edicion de la parte del cuerpo contenida por este slot
		/// </summary>
		public ViewModelCreacionEdicionParteDelCuerpo ViewModelCreacionEdicionParteDelCuerpo { get; private set; }

		/// <summary>
		/// Viewmodel para la creacion, edicion o vista de los items contenidos
		/// </summary>
		public ViewModelCreacionEdicionItem ViewModelCreacionEdicionItem { get; private set; }

		/// <summary>
		/// Viewmodel de la lista de items contenidos en el slot
		/// </summary>
		public ViewModelListaItems<ViewModelItemListaItems> ViewModelListaItemsSlot { get; private set; }

		/// <summary>
		/// Comando que se ejecuta cuado se presiona el boton 'Slot'
		/// </summary>
		public ICommand ComandoVerContenidoSlot { get; private set; }

		/// <summary>
		/// Comando que se ejecuta cuando se presiona el boton 'Contenido'
		/// </summary>
		public ICommand ComandoVerSlot { get; private set; }

		/// <summary>
		/// Comando que se ejecuta cuando se presiona el boton 'Crear Item'
		/// </summary>
		public ICommand ComandoCrearItem { get; private set; }

		/// <summary>
		/// Comando que se ejecuta cuando se presiona el boton 'Crear parte del cuerpo'
		/// </summary>
		public ICommand ComandoCrearParteDelCuerpo { get; private set; }

		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_accionSalir">Accion que se ejecuta al salir de este vm</param>
		/// <param name="_controladorParaEditar">Controlador del <see cref="ModeloSlot"/> que sera editado</param>
		public ViewModelCreacionEdicionDeSlot(Action<ViewModelCreacionEdicionDeSlot> _accionSalir, ControladorSlot _controladorParaEditar) 
			
			: base(_accionSalir, _controladorParaEditar, true, false)
		{
			CrearComandoEliminar();

			ComandoVerSlot = new Comando(() =>
			{
				MostrarSlot = true;
				MostrarContenido = false;
			});

			ComandoVerContenidoSlot = new Comando(() =>
			{
				MostrarSlot = false;
				MostrarContenido = true;
			});

			ComandoCrearItem = new Comando(async () =>
			{
				if(ViewModelListaItemsSlot == null)
					CrearViewModelListaItems();

				ViewModelListaItemsSlot.DelegadoAñadirItem();
			});

			ComandoCrearParteDelCuerpo = new Comando(async ()=>
			{
				var nuevaParteDelCuerpo = new ModeloParteDelCuerpo
				{
					Nombre = "HuesitoHuerfano",
					SlotContenedor = ControladorSiendoEditado.modelo,
					PersonajeContenedor = ControladorSiendoEditado.modelo.PersonajeContenedor,
					MultiplicadorDeEstaParte = 1
				};

				ModeloCreado.ParteDelCuerpoAlmacenada = nuevaParteDelCuerpo;

				ControladorSiendoEditado.AlmacenarParteDelCuerpo(new ControladorParteDelCuerpo(nuevaParteDelCuerpo));

				await SistemaPrincipal.GuardarModeloAsync(nuevaParteDelCuerpo);

				CrearViewModelCreacionEdicionPartedelCuerpo();

				DispararPropertyChanged(nameof(MostrarMenuCrearContenido));
			});

			mAccionSalirCreacionEdicionItem = async vm =>
			{
				if (vm.Resultado.EsAceptarOFinalizar())
				{
					var controladorCreado = vm.CrearControlador();

					await SistemaPrincipal.GuardarModeloAsync(controladorCreado.modelo);
					await SistemaPrincipal.GuardarDatosAsync();

					ControladorSiendoEditado.AlmacenarItem(controladorCreado);

					AñadirModeloDesdeListaItems<ModeloItem, ViewModelItemListaItems>((ViewModelItemListaItems)controladorCreado.CrearViewModelItem(), ModeloCreado.ItemsAlmacenados, ViewModelListaItemsSlot);

					DispararPropertyChanged(nameof(MostrarMenuCrearContenido));
				}
			};

			PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName != nameof(EsValido) && ModeloCreado != null)
					ActualizarValidez();

				if(args.PropertyName == nameof(MostrarMenuCrearContenido)) 
					DispararPropertyChanged(nameof(MostrarListaItems));
			};
		}

		public override async Task<ViewModelCreacionEdicionDeSlot> Inicializar(Type tipoValorPorDefectoModelo = null)
		{
			await base.Inicializar(tipoValorPorDefectoModelo);

			//Si el controlador no es null y no esta vacio...
			if (!(ControladorSiendoEditado?.EstaVacio ?? true))
			{
				//Si contiene items...
				if (ControladorSiendoEditado.ContieneItems)
				{
					CrearViewModelListaItems();

					ControladorSiendoEditado.ControladoresItemsAlmacenados.ForEach(i =>
					{
						AñadirModeloDesdeListaItems<ModeloItem, ViewModelItemListaItems>((ViewModelItemListaItems)i.CrearViewModelItem(), ModeloCreado.ItemsAlmacenados, ViewModelListaItemsSlot);
					});
				}
				//Si contiene una parte del cuerpo...
				else
				{
					CrearViewModelCreacionEdicionPartedelCuerpo();
				}
			}

			return this;
		}

		public override ModeloSlot CrearModelo()
		{
			throw new NotImplementedException();
		}

		public override ControladorSlot CrearControlador()
		{
			throw new NotImplementedException();
		}

		public override void ActualizarValidez()
		{
			if (NombreSlot.IsNullOrWhiteSpace() || ModeloCreado.EspacioTotal <= 0)
			{
				EsValido = false;

				return;
			}

			EsValido = true;
		}

		private async void CrearViewModelCreacionEdicionPartedelCuerpo()
		{
			ViewModelCreacionEdicionParteDelCuerpo =
				await new ViewModelCreacionEdicionParteDelCuerpo(async vm =>
				{
					if (vm.Resultado.EsAceptarOFinalizar())
					{
						var controladorCreado = vm.CrearControlador();

						if (!vm.EstaEditando)
						{
							await SistemaPrincipal.GuardarModeloAsync(controladorCreado.modelo);
							await SistemaPrincipal.GuardarDatosAsync();

							ControladorSiendoEditado.AlmacenarParteDelCuerpo(controladorCreado);

							DispararPropertyChanged(nameof(MostrarMenuCrearContenido));
						}
						else
						{
							var resultadoCopia = await vm.ModeloCreado.CrearCopiaProfundaEnSubtipoAsync<ModeloParteDelCuerpo, ModeloParteDelCuerpo>(controladorCreado.modelo);

							foreach (var modeloCreado in resultadoCopia.modelosCreadosEliminados.ModelosCreados)
							{
								if (modeloCreado is ModeloDatosDefensa datosDefensa)
									datosDefensa.ParteDelCuerpo = resultadoCopia.resultado;
							}

							await resultadoCopia.modelosCreadosEliminados.GuardarYEliminarModelosAsync();

							await controladorCreado.Recargar();
						}

						OnSlotModificado(ControladorSiendoEditado);
					}
					else if (vm.Resultado == EResultadoViewModel.Eliminar)
					{
						ViewModelCreacionEdicionParteDelCuerpo = null;

						ModeloCreado.ParteDelCuerpoAlmacenada = null;

						DispararPropertyChanged(nameof(MostrarMenuCrearContenido));

						OnSlotModificado(ControladorSiendoEditado);
					}

				}, ControladorSiendoEditado?.ParteDelCuerpoAlmacenada).Inicializar();
		}

		private void CrearViewModelListaItems()
		{
			ViewModelListaItemsSlot = new ViewModelListaItems<ViewModelItemListaItems>(async () =>
				{
					await MensajeHelpers.MostrarVentanaMensajeCreacionEdicionModelo(await new ViewModelCreacionEdicionItem(mAccionSalirCreacionEdicionItem, ControladorSiendoEditado.modelo.PersonajeContenedor ,null, ControladorSiendoEditado).Inicializar(), (int)SistemaPrincipal.Aplicacion.VentanaActual.ObtenerTamaño().Y - 100, (int)SistemaPrincipal.Aplicacion.VentanaActual.ObtenerTamaño().X - 200);
				},
				lista => ControladorSiendoEditado.EspacioDisponible > 0,
				ControladorSiendoEditado.EspacioDisponible > 0, "Items");

			ViewModelListaItemsSlot.Items.Elementos.CollectionChanged += (sender, args) =>
			{
				if (ViewModelListaItemsSlot.Items.Count <= 0)
				{
					DispararPropertyChanged(nameof(MostrarMenuCrearContenido));
				}

				OnSlotModificado(ControladorSiendoEditado);
			};
		}
	}
}