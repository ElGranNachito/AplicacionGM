using System;

namespace AppGM.Core
{
	/// <summary>
	/// Resultado de un <see cref="ViewModelConResultado"/>
	/// </summary>
	[Flags]
	public enum EResultadoViewModel
	{
		NoEstablecido = 1 << 0,

		Cancelar  = 1<<1,
		Aceptar   = 1<<2,
		Finalizar = 1<<3,
		Eliminar  = 1<<4,
	}
}
