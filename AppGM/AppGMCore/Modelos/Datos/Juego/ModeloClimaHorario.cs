using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    /// <summary>
    /// Representa el clima actual en un rol.
    /// </summary>
    public class ModeloClimaHorario : ModeloBase
    {
        /// <summary>
        /// Tipo de clima general en un rol.
        /// </summary>
        public EClima Clima { get; set; }

        /// <summary>
        /// Clase de viento general en un rol.
        /// </summary>
        public EViento Viento { get; set; }

        /// <summary>
        /// Valor estimativo de la temperatura general en un rol.
        /// </summary>
        public ETemperatura Temperatura { get; set; }
        
        /// <summary>
        /// Valor estimativo de la humedad general en un rol.
        /// </summary>
        public EHumedad Humedad { get; set; }

        /// <summary>
        /// Dia de la semana en la que se encuentra un rol.
        /// </summary>
        public EDiaSemana DiaSemana { get; set; }

        /// <summary>
        /// Hora del rol.
        /// </summary>
        public int Hora { get; set; }

        /// <summary>
        /// Minuto del rol.
        /// </summary>
        public int Minuto { get; set; }

        /// <summary>
        /// Clave foranea que referencia al <see cref="ModeloRol"/> al que pertenece este clima-horario
        /// </summary>
        [ForeignKey(nameof(RolAlQuePertenece))]
        public int? IdRol { get; set; }

        /// <summary>
        /// Rol al que pertenece este clima-horario
        /// </summary>
        public virtual ModeloRol RolAlQuePertenece { get; set; }
    }
}
