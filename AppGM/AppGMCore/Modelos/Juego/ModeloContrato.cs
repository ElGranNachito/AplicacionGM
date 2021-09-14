﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de datos para un contrato
    /// </summary>
    public class ModeloContrato : ModeloBase
    {
        /// <summary>
        /// Titulo del contrato
        /// </summary>
        [StringLength(50)]
        [MaxLength(50)]
        public string Nombre { get; set; }

        /// <summary>
        /// Descripcion del contrato.
        /// Deberia contener clausulas y demas datos de importancia
        /// </summary>
        [StringLength(1000)]
        [MaxLength(1000)]
        public string Descripcion { get; set; }

        /// <summary>
        /// <see cref="bool"/> indicando si aun esta vigente
        /// </summary>
        public bool EsVigente { get; set; }

        /// <summary>
        /// Personajes atados por este contrato
        /// </summary>
        public virtual List<TIPersonajeContrato> PersonajesAfectados { get; set; } = new List<TIPersonajeContrato>();
    }
}
