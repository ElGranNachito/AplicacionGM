﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de datos para el rol
    /// </summary>
    public class ModeloRol : ModeloBase
    {
        public ControladorRol controladorRol;

        //Dia dentro del mundo del rol
        public ushort Dia { get; set; }

        //Nombre del rol
        [StringLength(50)]
        public string Nombre { get; set; }

        //Descripcion del rol
        [StringLength(2000)]
        public string Descripcion { get; set; }

        //Anotaciones realizadas por el GM
        public string Registros { get; set; }

        // Ultima fecha de uso del rol (sesion)
        public DateTime FechaUltimaSesion { get; set; }

        public List<TIRolPersonaje> Personajes { get; set; } = new List<TIRolPersonaje>();
        public List<TIRolCombate> Combates { get; set; } = new List<TIRolCombate>();
        public List<TIRolMapa> Mapas { get; set; } = new List<TIRolMapa>();
    }
}
