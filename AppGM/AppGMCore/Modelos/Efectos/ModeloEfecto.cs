﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de datos para efecto
    /// </summary>
    public class ModeloEfecto : IDescripcion
    {
        //Id
        [Key]
        public int IdEfecto { get; set; }

        //Nombre del efecto
        [StringLength(50)]
        public string Nombre { get; set; }
        //Descripcion del efecto
        [StringLength(500)]
        public string Descripcion { get; set; }

        public List<TIEfectoModificadorDeStatBase> Modificaciones;
    }

    public class ModeloEfectoTemporal : ModeloEfecto
    {
        //Turnos que dura el efecto
        public ushort TurnosDeDuracion { get; set; }
    }
}