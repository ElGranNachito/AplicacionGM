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
		/// Obtiene la profundidad de este elemento en el inventario de un <see cref="ModeloPersonaje"/>
		/// </summary>
		/// <param name="profundidadActual"></param>
		/// <returns></returns>
		public int ObtenerProfundidad(int profundidadActual);

		/// <summary>
		/// Obtiene un nombre random para un nuevo slot
		/// </summary>
		/// <returns>Nombre para un nuevo slot</returns>
		public static string ObtenerNombreParaNuevoSlot(IModeloConSlots modelo)
		{
			var nombresRandom = new string[] {"Slot promedio", "Solo un slot", "Un slot salvaje", "Slotito", "Slots a domicilio", "Toc toc ¿quien es? un slot"};

			return $"{nombresRandom[new Random().Next(0, nombresRandom.Length)]} #{modelo.Slots.Count + 1}";
		}
	}
}
