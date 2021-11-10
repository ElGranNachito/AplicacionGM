﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
	/// <summary>
	/// Modelo que contiene los datos de un item defensivo
	/// </summary>
	public class ModeloDatosDefensivo : ModeloBase
	{
		/// <summary>
		/// Contiene los tipo de deteccion de daño que utiliza este item
		/// </summary>
		public EEstrategiaDeDeteccionDeDaño EstrategiasDeDeteccionDeDañoUtilizadas { get; set; }

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
		/// Daños que reduce
		/// </summary>
		public virtual List<ModeloDatosReduccionDeDaño> ReduccionesDeDaños { get; set; } = new List<ModeloDatosReduccionDeDaño>();
	}
}
