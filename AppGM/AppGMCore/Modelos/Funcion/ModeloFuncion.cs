using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
	/// <summary>
	/// Representa una funcion escrita en GuraScratch
	/// </summary>
	public class ModeloFuncion : ModeloConVariablesYTiradas
	{
		/// <summary>
		/// Padre de este modelo
		/// </summary>
		public virtual TIFuncionPadreFuncion Padre { get; set; }

		/// <summary>
		/// Modelo que contiene esta funcion
		/// </summary>
		public virtual TIFuncionContenedor ContenedorFuncion { get; set; }

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