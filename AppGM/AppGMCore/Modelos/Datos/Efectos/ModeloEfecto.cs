using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
	/// <summary>
	/// Modelo de datos para un efecto
	/// </summary>
	public class ModeloEfecto : ModeloBase
	{
		/// <summary>
		/// Turnos que dura el efecto
		/// </summary>
		public int TurnosDeDuracion { get; set; }

		/// <summary>
		/// Nombre del efecto
		/// </summary>
		[StringLength(50)]
		[MaxLength(50)]
		public string Nombre { get; set; }

		/// <summary>
		/// Descripcion
		/// </summary>
		[StringLength(500)]
        [MaxLength(500)]
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
		/// Contiene las funciones requeridas para el funcionamiento de este efecto
		/// </summary>
		public virtual List<TIFuncionEfecto> Funciones { get; set; } = new List<TIFuncionEfecto>();

		/// <summary>
		/// Handlers vinculados a eventos del controlador de este efecto
		/// </summary>
		public virtual List<TIFuncionHandlerEvento<ModeloEfecto>> HandlersEventos { get; set; } = new List<TIFuncionHandlerEvento<ModeloEfecto>>();

		/// <summary>
		/// Todas las aplicaciones de este efecto
		/// </summary>
		public virtual List<ModeloEfectoSiendoAplicado> Aplicaciones { get; set; } = new List<ModeloEfectoSiendoAplicado>();

		/// <summary>
		/// Habilidad que contiene este efecto
		/// </summary>
		public virtual ModeloHabilidad HabilidadContenedora { get; set; }
	}
}
