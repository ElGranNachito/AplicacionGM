using System.Collections.Generic;

namespace AppGM.Core
{
	public class ModeloFuncion : ModeloBase
	{
		/// <summary>
		/// Lista de las variables de la funcion cuyo valor no se resetea en cada llamada a la funcion
		/// </summary>
		public List<TIFuncionVariable> VariablesPersistentes { get; set; }

		public string NombreFuncion { get; set; }
	}
}