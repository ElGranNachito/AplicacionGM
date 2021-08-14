﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
	/// <summary>
	/// Modelo de datos para un efecto
	/// </summary>
	public class ModeloEfecto : ModeloBase
	{
		/// <summary>
		/// Controlador del efecto
		/// </summary>
		public ControladorEfecto controladorEfecto;

		/// <summary>
		/// Turnos que dura el efecto
		/// </summary>
		public int TurnosDeDuracion { get; set; }

		/// <summary>
		/// Nombre del efecto
		/// </summary>
		[StringLength(50)]
		public string Nombre { get; set; }

		/// <summary>
		/// Descripcion
		/// </summary>
		[StringLength(500)]
		public string Descripcion { get; set; }

		/// <summary>
		/// Tipo de este efecto
		/// </summary>
		public ETipoEfecto Tipo { get; set; }

		/// <summary>
		/// Comportamiento acumulativo de este efecto
		/// </summary>
		public EComportamientoAcumulativo ComportamientoAcumulativo { get; set; }

		/// <summary>
		/// Funcion que determina si se puede aplicar este efecto a un objetivo
		/// </summary>
		public TIEfectoFuncion FuncionPuedeAplicar { get; set; }

		/// <summary>
		/// Funcion que aplica este efecto
		/// </summary>
		public TIEfectoFuncion FuncionAplicar { get; set; }

		/// <summary>
		/// Funcion que quita este efecto
		/// </summary>
		public TIEfectoFuncion FuncionQuitar { get; set; }

		/// <summary>
		/// Todas las aplicaciones de este efecto
		/// </summary>
		public virtual List<TIEfectoSiendoAplicadoEfecto> Aplicaciones { get; set; } = new List<TIEfectoSiendoAplicadoEfecto>();
	}
}
