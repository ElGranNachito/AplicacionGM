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
        /// Nombre del mapa
        /// </summary>
        [MaxLength(50)]
        public string NombreMapa { get; set; }

        /// <summary>
        /// Imagen del mapa
        /// </summary>
        public byte[] Imagen { get; set; }

        /// <summary>
        /// Posiciones de las unidades dentro de este mapa
        /// </summary>
        public virtual List<ModeloUnidadMapa> PosicionesUnidades { get; set; }  = new List<ModeloUnidadMapa>();

        /// <summary>
        /// Ambiente de este mapa
        /// </summary>
        public virtual ModeloAmbiente Ambiente { get; set; }

        /// <summary>
        /// Rol al que pertenece este mapa
        /// </summary>
        public virtual ModeloRol Rol { get; set; }

        /// <summary>
        /// Combate al que pertenece este mapa
        /// </summary>
        public virtual ModeloAdministradorDeCombate CombateAlQuePertenece { get; set; }
    }
}
