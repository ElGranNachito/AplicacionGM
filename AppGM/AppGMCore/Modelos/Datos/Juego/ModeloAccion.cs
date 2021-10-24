﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de una accion que realiza un personaje durante un combate
    /// </summary>
    public class ModeloAccion : ModeloBase
    {
        /// <summary>
        /// Descripcion de la accion
        /// </summary>
        [StringLength(2000)]
        [MaxLength(2000)]
        public string Descripcion { get; set; }

        /// <summary>
        /// Participante que realizo la accion
        /// </summary>
        public virtual ModeloParticipante Participante { get; set; }
    }
}