using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    /// <summary>
    /// Representa un mapa
    /// </summary>
    public class ModeloMapa : ModeloBase
    {
        /// <summary>
        /// Rol al que pertenece este mapa
        /// </summary>
        public virtual TIRolMapa RolMapa { get; set; }

        /// <summary>
        /// Controlador del mapa
        /// </summary>
        public ControladorMapa controladorMapa;

        /// <summary>
        /// Nombre del mapa
        /// </summary>
        [MaxLength(50)]
        public string NombreMapa { get; set; }

        /// <summary>
        /// Formato de la imagen del mapa
        /// </summary>
        public EFormatoImagen EFormatoImagen { get; set; }

        /// <summary>
        /// Posiciones de las unidades dentro de este mapa
        /// </summary>
        public virtual List<TIMapaUnidadMapa> PosicionesUnidades { get; set; }  = new List<TIMapaUnidadMapa>();
    }
}
