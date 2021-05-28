using System.Collections.Generic;

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
        public TIRolMapa RolMapa { get; set; }

        /// <summary>
        /// Controlador del mapa
        /// </summary>
        public ControladorMapa controladorMapa;

        /// <summary>
        /// Nombre del mapa
        /// </summary>
        public string NombreMapa { get; set; }

        /// <summary>
        /// Formato de la imagen del mapa
        /// </summary>
        public EFormatoImagen EFormatoImagen { get; set; }

        /// <summary>
        /// Posiciones de las unidades dentro de este mapa
        /// </summary>
        public List<TIMapaUnidadMapa> PosicionesUnidades { get; set; }  = new List<TIMapaUnidadMapa>();
    }
}
