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
        /// Turnos que le restan al efecto
        /// </summary>
        public ushort TurnosRestantes { get; set; }

        /// <summary>
        /// Indica si esta siendo aplicado actualmente
        /// </summary>
        public bool EstaSiendoAplicado { get; set; }

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
        /// </summary>
        public List<TIEfectoModificadorDeStatBase> Modificaciones { get; set; } = new List<TIEfectoModificadorDeStatBase>();
    }
}
