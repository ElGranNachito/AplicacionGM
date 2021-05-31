using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        /// Hora dentro del rol
        /// </summary>
        public int Hour { get; set; }

        /// <summary>
        /// Dia dentro del rol
        /// </summary>
        public ushort Dia { get; set; }

        /// <summary>
        /// Nombre del rol
        /// </summary>
        [StringLength(50)]
        public string Nombre { get; set; }

        /// <summary>
        /// Descripcion del rol
        /// </summary>
        [StringLength(2000)]
        public string Descripcion { get; set; }

        /// <summary>
        /// Anotaciones realizadas por el GM
        /// </summary>
        public string Registros { get; set; }

        /// <summary>
        /// Fecha de la ultima sesion
        /// </summary>
        public DateTime FechaUltimaSesion { get; set; }

        /// <summary>
        /// Personajes que forman parte de este rol
        /// </summary>
        public List<TIRolPersonaje> Personajes { get; set; } = new List<TIRolPersonaje>();

        /// <summary>
        /// Combates que se han realizado o estan realizando en este rol
        /// </summary>
        public List<TIRolCombate>   Combates   { get; set; } = new List<TIRolCombate>();

        /// <summary>
        /// Mapas que se utilizan en este rol
        /// </summary>
        public List<TIRolMapa>      Mapas      { get; set; } = new List<TIRolMapa>();
    }
}
