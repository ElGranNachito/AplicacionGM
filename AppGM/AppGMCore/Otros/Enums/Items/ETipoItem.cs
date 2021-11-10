using System;

namespace AppGM.Core
{
	/// <summary>
	/// Indica el tipo de un item
	/// </summary>
	[Flags]
	public enum ETipoItem
	{
		/// <summary>
		/// Item comun y corriente
		/// </summary>
		Item = 1<<0,

		/// <summary>
		/// Armita
		/// </summary>
		Arma = 1<<1,

		/// <summary>
		/// Arma a distancia
		/// </summary>
		ArmaDistancia = 1<<2,

		/// <summary>
		/// Item destinado a la defensa
		/// </summary>
		Defensivo = 1<<3,

		/// <summary>
		/// Ropita
		/// </summary>
		Ropa = 1<<4,

		/// <summary>
		/// Un mystic code bien piola
		/// </summary>
		MysticCode = 1<<5
	}
}
