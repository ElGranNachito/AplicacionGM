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

		public static class Variables
		{
			public const int VariableDueña      = int.MinValue;
			public const int ParametrosCreados  = int.MinValue + 1;
			public const int ControladorFuncion = int.MinValue + 2;
		}
	}
}
