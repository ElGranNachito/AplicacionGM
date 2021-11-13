using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public abstract class ModeloPersonajeJugable : ModeloPersonaje
    {
        public EClaseServant ClaseServant { get; set; }

        /// <summary>
        /// Nombre real del persoane
        /// </summary>
        [StringLength(100)]
        public string NombreReal { get; set; }

        /// <summary>
        /// Caracteristicas del personaje
        /// </summary>
        public virtual ModeloCaracteristicas Caracteristicas { get; set; }
    }
}