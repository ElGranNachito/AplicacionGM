using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace AppGM.Core
{
	/// <summary>
	/// <see cref="ViewModel"/> que representa el control de creacion de <see cref="ModeloParteDelCuerpo"/>
	/// </summary>
	public class ViewModelCreacionPartesDelCuerpo : ViewModelConResultado<ViewModelCreacionPartesDelCuerpo>
	{
		/// <summary>
		/// Personaje para el que se estan creando las partes
		/// </summary>
		public ModeloPersonaje Personaje { get; init; }

		/// <summary>
		/// Viewmodel del inventario
		/// </summary>
		public ViewModelInventario ViewModelInventario { get; set; }

		/// <summary>
		/// Indica si la ventana de seleccion de plantilla debe ser visible
		/// </summary>
		public bool DebeMostrarVentanaEleccionPlantilla { get; private set; }

		/// <summary>
		/// Comando que se ejecuta al presionar el boton 'Plantilla Humanoide'
		/// </summary>
		public ICommand ComandoCrearPlantillaHumanoide { get; private set; }

		/// <summary>
		/// Comando que se ejecuta al presionar el boton 'Plantilla Cuadrupedo'
		/// </summary>
		public ICommand ComandoCrearPlantillaCuadrupedo { get; private set; }

		/// <summary>
		/// Comando que se ejecuta al presionar el boton 'Crear Vacio'
		/// </summary>
		public ICommand ComandoCrearVacio { get; private set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_personaje"></param>
		public ViewModelCreacionPartesDelCuerpo(Action<ViewModelCreacionPartesDelCuerpo> _accionSalir, ModeloPersonaje _personaje)

			:base(_accionSalir)
		{
			Personaje = _personaje;

			ComandoCrearPlantillaHumanoide  = new Comando(AplicarPlantillaHumanoide);
			ComandoCrearPlantillaCuadrupedo = new Comando(AplicarPlantillaCuadrupedo);

			ComandoCrearVacio = new Comando(() =>
			{
				DebeMostrarVentanaEleccionPlantilla = false;
			});

			if (Personaje.SlotsBase.Count == 0)
			{
				DebeMostrarVentanaEleccionPlantilla = true;
			}
			else
			{
				CrearVMInventario();
			}
		}

		private void CrearVMInventario()
		{
			DebeMostrarVentanaEleccionPlantilla = false;

			ViewModelInventario = new ViewModelInventario(Personaje, null);
		}

		private async void AplicarPlantillaHumanoide()
		{
			var slotInicial = new ModeloSlot
			{
				NombreSlot = "SlotTorso",
				PersonajeDueño = Personaje
			};

			var torso = new ModeloParteDelCuerpo
			{
				Nombre = "Torso",
				MultiplicadorDeEstaParte = 1,
				PersonajeContenedor = Personaje,
				SlotContenedor = slotInicial
			};

			slotInicial.ParteDelCuerpoAlmacenada = torso;

			Personaje.SlotsBase.Add(slotInicial);
			Personaje.PartesDelCuerpo.Add(torso);

			var partesDelCuerpoTorso = new List<ModeloParteDelCuerpo>
			{
				new ModeloParteDelCuerpo
				{
					Nombre = "Brazo derecho",
					MultiplicadorDeEstaParte = 1,
					PersonajeContenedor = Personaje
				},

				new ModeloParteDelCuerpo
				{
					Nombre = "Brazo izquierdo",
					MultiplicadorDeEstaParte = 1,
					PersonajeContenedor = Personaje
				},

				new ModeloParteDelCuerpo
				{
					Nombre = "Pierna derecha",
					MultiplicadorDeEstaParte = 1,
					PersonajeContenedor = Personaje
				},

				new ModeloParteDelCuerpo
				{
					Nombre = "Pierna izquierda",
					MultiplicadorDeEstaParte = 1,
					PersonajeContenedor = Personaje
				},

				new ModeloParteDelCuerpo
				{
					Nombre = "Cuello",
					MultiplicadorDeEstaParte = 2,
					PersonajeContenedor = Personaje
				},
			};

			torso.Slots = new List<ModeloSlot>
			{
				new ModeloSlot
				{
					NombreSlot = "SlotTorso_BrazoDerecho",
					ParteDelCuerpoDueña = torso,
				},

				new ModeloSlot
				{
					NombreSlot = "SlotTorso_BrazoIzquierdo",
					ParteDelCuerpoDueña = torso,
				},

				new ModeloSlot
				{
					NombreSlot = "SlotTorso_PiernaDerecha",
					ParteDelCuerpoDueña = torso,
				},

				new ModeloSlot
				{
					NombreSlot = "SlotTorso_PiernaIzquierda",
					ParteDelCuerpoDueña = torso,
				},

				new ModeloSlot
				{
					NombreSlot = "SlotTorso_Cuello",
					ParteDelCuerpoDueña = torso,
				}
			};

			for (int i = 0; i < partesDelCuerpoTorso.Count; ++i)
			{
				partesDelCuerpoTorso[i].SlotContenedor = torso.Slots[i];
				torso.Slots[i].ParteDelCuerpoAlmacenada = partesDelCuerpoTorso[i];
			}

			await SistemaPrincipal.GuardarModelosAsync(Personaje.SlotsBase);
			await SistemaPrincipal.GuardarModelosAsync(Personaje.PartesDelCuerpo);
			await SistemaPrincipal.GuardarModelosAsync(torso.Slots);
			await SistemaPrincipal.GuardarModelosAsync(partesDelCuerpoTorso);

			await SistemaPrincipal.GuardarDatosAsync();

			CrearVMInventario();
		}

		private void AplicarPlantillaCuadrupedo()
		{
			CrearVMInventario();
		}
	}
}
