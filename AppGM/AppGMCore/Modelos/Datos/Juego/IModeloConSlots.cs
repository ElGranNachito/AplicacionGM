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
	}
}
