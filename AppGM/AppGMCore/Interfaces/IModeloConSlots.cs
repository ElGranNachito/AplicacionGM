using System;
using System.Collections.Generic;

namespace AppGM.Core
{
	/// <summary>
	/// Modelo que contiene slots
	/// </summary>
	public interface IModeloConSlots
	{
		/// <summary>
		/// Slots que contiene el modelo
		/// </summary>
		public List<ModeloSlot> Slots { get; set; }

		/// <summary>
		/// Obtiene un nombre random para un nuevo slot
		/// </summary>
		/// <returns>Nombre para un nuevo slot</returns>
		public static string ObtenerNombreParaNuevoSlot(IModeloConSlots modelo)
		{
			var nombresRandom = new string[5] {"Slot promedio", "Solo un slot", "Un slot salvaje", "Slotito", "Slots a domicilio"};

			return $"{nombresRandom[new Random().Next(0, 5)]} #{modelo.Slots.Count}";
		}
	}
}
