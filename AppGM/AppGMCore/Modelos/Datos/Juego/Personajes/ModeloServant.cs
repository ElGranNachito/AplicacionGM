﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public class ModeloServant : ModeloPersonajeJugable
    {
        public ERango RangoNP { get; set; }

        /// <summary>
        /// Energia magica del servant
        /// </summary>
        public int Prana       { get; set; }
        public int PranaActual { get; set; }

        /// <summary>
        /// Origenes de la leyenda del personaje
        /// </summary>
        [MaxLength(50)]
        public string Fuente { get; set; }

        /// <summary>
        /// NoblePhantasms que posee el servant
        /// </summary>
        public virtual List<ModeloNoblePhantasm> NoblePhantasms { get; set; } = new List<ModeloNoblePhantasm>();
    }
}