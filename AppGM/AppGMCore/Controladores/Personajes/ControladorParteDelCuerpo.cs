using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Controlador para un <see cref="ModeloParteDelCuerpo"/>
	/// </summary>
	public class ControladorParteDelCuerpo : Controlador<ModeloParteDelCuerpo>
	{
		public delegate void dParteDelCuerpoEliminada(ControladorParteDelCuerpo parteDelCuerpo, ControladorPersonaje dueño);

		public event dParteDelCuerpoEliminada OnParteDelCuerpoEliminada = delegate { };

		#region Propiedades

		/// <summary>
		/// Controlador del personaje a quien pertenece esta parte del cuerpo
		/// </summary>
		public ControladorPersonaje PersonajeContenedor { get; private set; }

		/// <summary>
		/// Controlador del slot que contiene esta parte del cuerpo
		/// </summary>
		public ControladorSlot SlotContenedor { get; private set; }

		/// <summary>
		/// Coleccion con los controladores de los slots contenidos por esta parte del cuerpo
		/// </summary>
		public List<ControladorSlot> Slots { get; set; } = new List<ControladorSlot>();

		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_modelo">Modelo de la parte del cuerpo contenida por este controlador</param>
		public ControladorParteDelCuerpo(ModeloParteDelCuerpo _modelo)
			:base(_modelo)
		{
			//Creamos el controlador de los slots contenidos
			foreach (var slot in modelo.Slots)
			{
				Slots.Add(SistemaPrincipal.ObtenerControlador<ControladorSlot, ModeloSlot>(slot, true));
			}

			//Intentamos obtener al personaje contendor
			PersonajeContenedor = SistemaPrincipal.ObtenerControlador<ControladorPersonaje, ModeloPersonaje>(modelo.PersonajeContenedor);

			if (PersonajeContenedor == null)
			{
				modelo.PersonajeContenedor.OnControladorCreado += ControladorParaModeloCreadoHandler;
			}

			//Intentamos obtener el slot que nos contiene
			SlotContenedor = SistemaPrincipal.ObtenerControlador<ControladorSlot, ModeloSlot>(modelo.SlotContenedor);

			if (SlotContenedor == null)
			{
				modelo.SlotContenedor.OnControladorCreado += ControladorParaModeloCreadoHandler;
			}

			modelo.AñadirHandlerSoloUsoModeloEliminado(m =>
			{
				OnParteDelCuerpoEliminada(this, PersonajeContenedor);
			});
		}

		public override async Task Recargar()
		{
			await base.Recargar();

			if (modelo.Slots.Count > Slots.Count)
			{
				var diferencia = modelo.Slots.Count - Slots.Count;

				for (int i = modelo.Slots.Count - diferencia; i < diferencia; ++i)
					Slots.Add(SistemaPrincipal.ObtenerControlador<ControladorSlot, ModeloSlot>(modelo.Slots[i], true));
			}
		}

		protected override void ControladorParaModeloCreadoHandler(ModeloBase modelo, ControladorBase controlador)
		{
			base.ControladorParaModeloCreadoHandler(modelo, controlador);

			switch (modelo)
			{
				case ModeloPersonaje pj:
					PersonajeContenedor = (ControladorPersonaje) controlador;
					break;

				case ModeloSlot slot:
					SlotContenedor = (ControladorSlot) controlador;
					break;

				default:
					SistemaPrincipal.LoggerGlobal.Log($"{nameof(modelo)} no es de un tipo soportado", ESeveridad.Error);
					break;
			}
		}
	}
}