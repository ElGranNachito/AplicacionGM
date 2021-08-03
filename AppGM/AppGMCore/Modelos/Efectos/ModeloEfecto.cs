using System.Collections.Generic;
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
		public ushort TurnosDeDuracion { get; set; }

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
		/// Modificaciones que aplica el efecto
		/// TODO: Quitar esto, te lo dejo a vos Contykpo para no cagarte el RolContext UwU. Att: Creisi como vos dirias
		/// </summary>
		public virtual List<TIEfectoModificadorDeStatBase> Modificaciones { get; set; } = new List<TIEfectoModificadorDeStatBase>();

		/// <summary>
		/// Todas las aplicaciones de este efecto
		/// </summary>
		public virtual List<TIEfectoSiendoAplicadoEfecto> Aplicaciones { get; set; } = new List<TIEfectoSiendoAplicadoEfecto>();
	}
}
