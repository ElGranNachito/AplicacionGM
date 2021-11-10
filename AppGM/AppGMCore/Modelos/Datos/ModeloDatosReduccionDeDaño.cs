using System.Collections.Generic;

namespace AppGM.Core
{
	/// <summary>
	/// Modelo que contiene datos detallando las facultades de reduccion de daño de cierto <see cref="ModeloItem"/> o <see cref="ModeloParteDelCuerpo"/>
	/// </summary>
	public class ModeloDatosReduccionDeDaño : ModeloBase
	{
		/// <summary>
		/// Valor de la reduccion
		/// </summary>
		public decimal ValorReduccion { get; set; }

		/// <summary>
		/// Manera en la que se detecta el daño que reducir
		/// </summary>
		public EEstrategiaDeDeteccionDeDaño EstrategiaDeDeteccionDeDaño { get; set; }

		/// <summary>
		/// Manera en la que reduce el daño
		/// </summary>
		public EMetodoDeReduccionDeDaño MetodoDeReduccionDeDaño { get; set; }

		/// <summary>
		/// Tipo del daño que reduce
		/// </summary>
		public ETipoDeDaño TipoDeDañoQueReduce { get; set; }

		/// <summary>
		/// Rango del daño que reduce
		/// </summary>
		public ERango RangoDelDañoQueReduce { get; set; }

		/// <summary>
		/// Nivel de la magia cuyo daño reduce
		/// </summary>
		public ENivelMagia NivelDeLaMagiaCuyoDañoReduce { get; set; }

		/// <summary>
		/// Fuentes de daño que abarca la reduccion
		/// </summary>
		public virtual List<ModeloFuenteDeDaño> FuentesDeDañoQueReduce { get; set; }
	}
}
