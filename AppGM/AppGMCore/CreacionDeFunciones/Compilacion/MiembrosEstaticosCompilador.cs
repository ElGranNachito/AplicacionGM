using System.Collections.Generic;

namespace AppGM.Core
{
	//TODO: Ver si en verdad es necesaria esta clase
	public sealed partial class Compilador
	{
		public static Compilador IniciarCompilacion(List<BloqueBase> bloques)
		{
			//TODO: Implementar bien
			return new Compilador(bloques);
		}

		public static readonly string NombreVariableDueña = "DueñoFuncion";
	}
}
