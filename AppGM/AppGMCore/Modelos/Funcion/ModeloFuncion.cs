using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
	public class ModeloFuncion : ModeloBase
	{
		/// <summary>
		/// Lista de las variables de la funcion cuyo valor no se resetea en cada llamada a la funcion
		/// </summary>
		public virtual List<TIFuncionVariable> VariablesPersistentes { get; set; }

		/// <summary>
		/// Padre de este modelo
		/// </summary>
		public virtual TIFuncionPadreFuncion Padre { get; set; }

		public string NombreFuncion { get; set; }

		/// <summary>
		/// Devuelve la id de la funcion utilizada para identificar al archivo XML
		/// </summary>
		[NotMapped]
		public int IDFuncion
		{
			get
			{
				if (Padre != null)
					return Padre.IDPadre;

				return Id;
			}
		}
	}
}