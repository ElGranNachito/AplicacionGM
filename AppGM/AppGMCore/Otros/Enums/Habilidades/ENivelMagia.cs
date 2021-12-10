using System;

namespace AppGM.Core
{
	/// <summary>
	/// Representa el nivel de una magia
	/// </summary>
	public enum ENivelMagia
	{
		NINGUNO = 0,

		Cero   = 1,
		Uno    = 2,
		Dos    = 3,
		Tres   = 4,
		Cuatro = 5,
		Cinco  = 6,
		Seis   = 7,
		Siete  = 8,
		Ocho   = 9
	}

	/// <summary>
	/// Representa el nivel de una magia
	/// </summary>
	[Flags]
	public enum ENivelMagiaFlags
	{
		NINGUNO = 0,

		Cero   = 1 << 0,
		Uno    = 1 << 1,
		Dos    = 1 << 2,
		Tres   = 1 << 3,
		Cuatro = 1 << 4,
		Cinco  = 1 << 5,
		Seis   = 1 << 6,
		Siete  = 1 << 7,
		Ocho   = 1 << 8
	}
}
