using System.Collections.Generic;

namespace AppGM.Core
{
	/// <summary>
	/// Controlador para un <see cref="ModeloParteDelCuerpo"/>
	/// </summary>
	public class ControladorParteDelCuerpo : Controlador<ModeloParteDelCuerpo>
	{
		public List<ControladorSlot> Slots { get; set; } = new List<ControladorSlot>();

		public ControladorParteDelCuerpo(ModeloParteDelCuerpo _modelo)
			:base(_modelo)
		{
			foreach (var slot in modelo.Slots)
			{
				Slots.Add(SistemaPrincipal.ObtenerControlador<ControladorSlot, ModeloSlot>(slot, true));
			}
		}
	}
}
