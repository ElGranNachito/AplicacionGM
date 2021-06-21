using System.Collections.Generic;

namespace AppGM.Core
{
	public sealed partial class Compilador
	{
		public static Compilador IniciarCompilacion(List<BloqueBase> bloques)
		{
			//TODO: Implementar bien
			return new Compilador(null, bloques);
		}
	}
}
