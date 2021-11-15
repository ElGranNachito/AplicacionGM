using System.Collections.Generic;
using System.Threading.Tasks;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Controlador para un <see cref="ModeloParteDelCuerpo"/>
	/// </summary>
	public class ControladorParteDelCuerpo : Controlador<ModeloParteDelCuerpo>, IDañable
	{
		#region Eventos

		public delegate void dParteDelCuerpoEliminada(ControladorParteDelCuerpo parteDelCuerpo, ControladorPersonaje dueño);

		/// <summary>
		/// Evento disparado cuando el <see cref="ModeloParteDelCuerpo"/> representado por este controlador es eliminado
		/// </summary>
		public event dParteDelCuerpoEliminada OnParteDelCuerpoEliminada = delegate { };

		public event IDañable.dDañado OnDañado; 

		#endregion

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

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_modelo">Modelo de la parte del cuerpo contenida por este controlador</param>
		public ControladorParteDelCuerpo(ModeloParteDelCuerpo _modelo)
			: base(_modelo)
		{
			//Creamos el controlador de los slots contenidos
			foreach (var slot in modelo.Slots)
			{
				AñadirControladorSlot(SistemaPrincipal.ObtenerControlador<ControladorSlot, ModeloSlot>(slot, true));
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

		#endregion

		#region Metodos

		public void Dañar(ModeloArgumentosDaño argsDaño, SortedList<int, IDañable> subObjetivos = null)
		{
			throw new System.NotImplementedException();
		}

		public override async Task Recargar()
		{
			await base.Recargar();

			if (modelo.Slots.Count > Slots.Count)
			{
				var diferencia = modelo.Slots.Count - Slots.Count;

				for (int i = modelo.Slots.Count - diferencia; i < modelo.Slots.Count; ++i)
					AñadirControladorSlot(SistemaPrincipal.ObtenerControlador<ControladorSlot, ModeloSlot>(modelo.Slots[i], true));

				SistemaPrincipal.LoggerGlobal.Log($"Añadidos {diferencia} nuevos slots", ESeveridad.Debug);
			}
		}

		protected override void ControladorParaModeloCreadoHandler(ModeloBase modelo, ControladorBase controlador)
		{
			base.ControladorParaModeloCreadoHandler(modelo, controlador);

			switch (modelo)
			{
				case ModeloPersonaje pj:
					PersonajeContenedor = (ControladorPersonaje)controlador;
					break;

				case ModeloSlot slot:
					SlotContenedor = (ControladorSlot)controlador;
					break;

				default:
					SistemaPrincipal.LoggerGlobal.Log($"{nameof(modelo)} no es de un tipo soportado", ESeveridad.Error);
					break;
			}
		}

		/// <summary>
		/// Añade un nuevo <see cref="ControladorSlot"/> a <see cref="Slots"/>
		/// </summary>
		/// <param name="slot">Slot que añadir</param>
		private void AñadirControladorSlot(ControladorSlot slot)
		{
			if (slot == null)
				SistemaPrincipal.LoggerGlobal.LogCrash($"{nameof(slot)} no puede ser null");

			Slots.Add(slot);

			slot.modelo.AñadirHandlerSoloUsoModeloEliminado(m => Slots.Remove(slot));
		} 

		#endregion
	}
}