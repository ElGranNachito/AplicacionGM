﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de datos para el rol
    /// </summary>
    public class ModeloRol : ModeloBase
    {
        /// <summary>
        /// Controlador
        /// </summary>
        public ControladorRol controladorRol;

        /// <summary>
        /// Temporada del año en la que se encuentra el rol
        /// </summary>
        public ETemporada Temporada { get; set; }
        /// <summary>
        /// Condiciones climaticas actuales dentro del rol
        /// </summary>
        public ECondicionClimatica CondicionClimatica { get; set; }

        /// <summary>
        /// Hora dentro del rol
        /// </summary>
        public int Hora { get; set; }
        /// <summary>
        /// Dia dentro del rol
        /// </summary>
        public ushort Dia { get; set; }

        /// <summary>
        /// Nombre del rol
        /// </summary>
        [StringLength(50)]
        [MaxLength(50)]
        public string Nombre { get; set; }

        /// <summary>
        /// Descripcion del rol
        /// </summary>
        [StringLength(3500)]
        [MaxLength(3500)]
        public string Descripcion { get; set; }

        /// <summary>
        /// Anotaciones realizadas por el GM
        /// TODO: Cambiar una vez se implemente el sistema de registros
        /// </summary>
        [MaxLength(4000)]
        public string Registros { get; set; }

        /// <summary>
        /// Fecha de la ultima sesion
        /// </summary>
        public DateTime FechaUltimaSesion { get; set; }

        /// <summary>
        /// Ambiente general dentro del area de este rol
        /// </summary>
        public virtual ModeloAmbiente AmbienteGlobal { get; set; }

        /// <summary>
        /// Clima horario general dentro del area de este rol.
        /// </summary>
        public virtual ModeloClimaHorario ClimaHorarioGlobal { get; set; }

        /// <summary>
        /// Fuentes de daño existentes en este rol
        /// </summary>
        public virtual List<ModeloFuenteDeDaño> FuentesDeDaño { get; set; } = new List<ModeloFuenteDeDaño>();

        /// <summary>
        /// Personajes que forman parte de este rol
        /// </summary>
        public virtual List<ModeloPersonaje> Personajes { get; set; } = new List<ModeloPersonaje>();

        /// <summary>
        /// Combates que se han realizado o estan realizando en este rol
        /// </summary>
        public virtual List<ModeloAdministradorDeCombate> Combates { get; set; } = new List<ModeloAdministradorDeCombate>();

        /// <summary>
        /// Mapas que se utilizan en este rol
        /// </summary>
        public virtual List<ModeloMapa> Mapas { get; set; } = new List<ModeloMapa>();

    }
}
