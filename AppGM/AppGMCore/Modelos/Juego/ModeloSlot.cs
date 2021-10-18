using System.Collections.Generic;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de un slot. Se trata de un espacio perteciente a algun <see cref="ModeloUtilizable"/> donde se pueden almacenar 
    /// objetos
    /// </summary>
    public class ModeloSlot : ModeloBase
    {
        /// <summary>
        /// Espacio total que ofrece el slot
        /// </summary>
        public decimal EspacioTotal      { get; set; }

        /// <summary>
        /// Espacio disponible en el slot
        /// </summary>
        public decimal EspacioDisponible { get; set; }

        /// <summary>
        /// Items actualmente almacenados en el slot
        /// </summary>
        public virtual List<ModeloItem> ItemsAlmacenados { get; set; } = new List<ModeloItem>();

        /// <summary>
        /// Portable al que pertenece este slot
        /// </summary>
        public virtual ModeloPortable Dueño { get; set; }
    }
}
