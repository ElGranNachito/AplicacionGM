using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
	/// <summary>
	/// Modelo que representa los argumentos de una tirada
	/// </summary>
	public partial class ModeloHistorialTirada : ModeloBase
	{
		/// <summary>
		/// Mutliplicador de especialidad
		/// </summary>
		public int MultiplicadorEspecialidad { get; set; }

		/// <summary>
		/// Modificador de la tirada
		/// </summary>
		public int Modificador { get; set; }

		/// <summary>
		/// Resultado de la tirada
		/// </summary>
		public int Resultado { get; set; }

		/// <summary>
		/// Clave foranea de <see cref="ArgumentosDaño"/>
		/// </summary>
		[ForeignKey(nameof(ArgumentosDaño))]
		public int IdArgumentosDaño { get; set; }

		/// <summary>
		/// Multiplicador de la tirada
		/// </summary>
		public float Mutliplicador { get; set; }

		/// <summary>
		/// Parametro extra de la tirada
		/// </summary>
		[StringLength(128)]
		public string ArgumentoExtra { get; set; }

		/// <summary>
		/// Resultado detallado
		/// </summary>
		[StringLength(256)]
		public string ResultadoDetallado { get; set; }

		/// <summary>
		/// Mano utilizada
		/// </summary>
		public EManoUtilizada ManoUtilizada { get; set; }

		/// <summary>
		/// Stat utilizada
		/// </summary>
		public EStat Stat { get; set; }

		/// <summary>
		/// Tipo de la tirada
		/// </summary>
		public ETipoTirada TipoTirada { get; set; }

		/// <summary>
		/// Tirada representada
		/// </summary>
		public virtual ModeloTiradaBase TiradaRepresentada { get; set; }

		/// <summary>
		/// Argumentos del daño infligido
		/// </summary>
		public virtual ModeloArgumentosDaño ArgumentosDaño { get; set; }

		/// <summary>
		/// Inglidores del daño
		/// </summary>
		public virtual List<ModeloInfligidorDaño> InfligidoresDaño { get; set; }

		/// <summary>
		/// Objetivos del daño
		/// </summary>
		public virtual List<ModeloDañable> Objetivos { get; set; }
	}
}
