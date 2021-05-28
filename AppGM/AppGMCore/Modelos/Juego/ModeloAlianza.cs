using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de datos para una alianza
    /// </summary>
    public class ModeloAlianza : ModeloBase
    {
        /// <summary>
        /// Nombre de la alianza
        /// </summary>
        [StringLength(50)]
        public string Nombre { get; set; }

        /// <summary>
        /// Descripcion de la alianza
        /// </summary>
        [StringLength(500)]
        public string Descripcion { get; set; }

        /// <summary>
        /// <see cref="bool"/> indicando si actualmente esta vigente
        /// </summary>
        public bool EsVigente { get; set; }

        /// <summary>
        /// <see cref="ModeloContrato"/> de la alianza. Puede ser no haber
        /// </summary>
        public TIAlianzaContrato ContratoDeAlianza { get; set; }

        /// <summary>
        /// <see cref="ModeloPersonajeJugable"/> que forman parte de esta alianza
        /// </summary>
        public List<TIPersonajeAlianza> PersonajesAfectados { get; set; } = new List<TIPersonajeAlianza>();
    }
}
