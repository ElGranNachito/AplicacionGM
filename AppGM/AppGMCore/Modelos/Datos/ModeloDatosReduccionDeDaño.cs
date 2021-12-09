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
		/// Nombre de esta reduccion
		/// </summary>
		public string Nombre { get; set; }

		/// <summary>
		/// Indica si esta reduccion esta habilitada
		/// </summary>
		public bool EstaHabilitada { get; set; } = true;

		/// <summary>
		/// Manera en la que se detecta el daño que reducir
		/// </summary>
		public EEstrategiaDeDeteccionDeDaño EstrategiaDeDeteccionDeDaño { get; set; }

		/// <summary>
		/// Manera en la que reduce el daño
		/// </summary>
		public EMetodoDeReduccionDeDaño MetodoDeReduccionDeDaño { get; set; }

		/// <summary>
		/// Tipos de los daños que reduce
		/// </summary>
		public ETipoDeDaño TiposDeDañoQueReduce { get; set; }

		/// <summary>
		/// Rangos de los daños que reduce
		/// </summary>
		public ERangoFlags RangosDelDañoQueReduce { get; set; }

		/// <summary>
		/// Niveles de las magias cuyos daños reduce
		/// </summary>
		public ENivelMagiaFlags NivelesDeLasMagiasCuyosDañosReduce { get; set; }

		/// <summary>
		/// <see cref="ModeloDatosDefensa"/> al que pertenece
		/// </summary>
		public virtual ModeloDatosDefensa DatosDefensaAlQuePertenece { get; set; }

		/// <summary>
		/// Fuentes de daño que abarca la reduccion
		/// </summary>
		public virtual List<ModeloFuenteDeDaño> FuentesDeDañoQueReduce { get; set; } = new List<ModeloFuenteDeDaño>();
	}
}
