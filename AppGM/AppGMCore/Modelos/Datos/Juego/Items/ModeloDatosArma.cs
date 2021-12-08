using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
	/// <summary>
	/// Modelo que contiene los datos de un arma
	/// </summary>
	public class ModeloDatosArma : ModeloBase
	{
		/// <summary>
		/// Numero de cargadores que tiene este arma
		/// </summary>
		public int NumeroDeCargadores { get; set; }

		/// <summary>
		/// Numero de municiones por cagador que tiene este arma
		/// </summary>
		public int NumeroDeMunicionesPorCargador { get; set; }

		/// <summary>
		/// Indica si este arma ignora a la defensa
		/// </summary>
		public bool IgnoraDefensa { get; set; }

		/// <summary>
		/// Indica si este arma ignora a la defensa
		/// </summary>
		public bool TieneMunicion { get; set; }

		/// <summary>
		/// Tipos de daño que inflige este arma
		/// </summary>
		public ETipoDeDaño TiposDeDañoQueInflige { get; set; }

		/// <summary>
		/// Clave foranea del <see cref="Item"/>
		/// </summary>
		[ForeignKey(nameof(Item))]
		public int IDItem { get; set; }

		/// <summary>
		/// Item al que pertenece
		/// </summary>
		public virtual ModeloItem Item { get; set; }

		/// <summary>
		/// Fuentes de daño que abarcan este arma
		/// </summary>
		public virtual List<ModeloFuenteDeDaño> FuentesDeDañoQueAbarcaEsteArma { get; set; } = new List<ModeloFuenteDeDaño>();
	}
}