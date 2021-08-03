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
        /// Controlador
        /// </summary>
        public ControladorAlianza controladorAlianza;

        /// <summary>
        /// Tipo de icono que tendra la alianza como identificador de la misma.
        /// </summary>
        public EIconoAlianza EIconoAlianza { get; set; }

        /// <summary>
        /// Formato de la imagen asignada al icono de la alianza.
        /// </summary>
        public EFormatoImagen FormatoImagen { get; set; }

        /// <summary>
        /// Imagen para el icono de la alianza.
        /// </summary>
        public string PathImagenIcono { get; set; }

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
        /// <see cref="bool"/> Indicando si esta actualmente vigente
        /// </summary>
        public bool EsVigente { get; set; }

        /// <summary>
        /// <see cref="ModeloContrato"/> de la alianza.
        /// Es opcional, puede no haberlo
        /// </summary>
        public virtual TIAlianzaContrato ContratoDeAlianza { get; set; }

        /// <summary>
        /// <see cref="ModeloPersonajeJugable"/> que forman parte de esta alianza
        /// </summary>
        public virtual List<TIPersonajeAlianza> PersonajesAfectados { get; set; } = new List<TIPersonajeAlianza>();
    }
}
