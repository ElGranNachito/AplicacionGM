using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
	/// <summary>
	/// Clase que representa a un preset para una tirada
	/// </summary>
	public class ModeloPresetTirada
	{
		/// <summary>
		/// Nombre del preset
		/// </summary>
		[StringLength(32)]
		public string Nombre { get; set; }

		/// <summary>
		/// Descripcion del preset
		/// </summary>
		[StringLength(256)]
		public string Descripcion { get; set; }

		/// <summary>
		/// Valor de la variable
		/// </summary>
		[StringLength(128)]
		public string VariableExtra { get; set; }

		/// <summary>
		/// Ids de las fuentes de daño abarcadas, separadas por ';'
		/// </summary>
		[Column(TypeName = "varchar(256)")]
		public string FuentesDeDañoAbarcadas { get; set; }

		/// <summary>
		/// Tirada para obtener el numero de tiradas
		/// </summary>
		[StringLength(256)]
		public string NumeroDeTiradas { get; set; }

		/// <summary>
		/// Modificador
		/// </summary>
		public int Modificador { get; set; }

		/// <summary>
		/// Multiplicador de especialidad
		/// </summary>
		public int? MultiplicadorDeEspecialidad { get; set; }

		/// <summary>
		/// Indica si se debe utilizar el multiplicador de punto vital
		/// </summary>
		public bool UtilizaMultiplicadorDelPuntoVital { get; set; }

		/// <summary>
		/// Tipo de daño de la tirada
		/// </summary>
		public ETipoDeDaño? TipoDeDaño { get; set; }

		/// <summary>
		/// Stat de la tirada
		/// </summary>
		public EStat? Stat { get; set; }

		/// <summary>
		/// Mano utilizada
		/// </summary>
		public EManoUtilizada? ManoUtilizada { get; set; }

		/// <summary>
		/// Tirada a la que pertenece este preset
		/// </summary>
		public virtual ModeloTiradaBase TiradaALaQuePertenece { get; set; }
	}
}