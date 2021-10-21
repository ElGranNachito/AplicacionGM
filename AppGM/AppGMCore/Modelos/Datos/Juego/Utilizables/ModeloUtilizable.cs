using System.Collections.Generic;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de datos para el utilizable
    /// </summary>
    public partial class ModeloUtilizable : ModeloConVariablesYTiradas
    {
	    /// <summary>
        /// Peso del utilizable
        /// </summary>
        public decimal Peso { get; set; }

        /// <summary>
        /// Nombre del utilizable
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Descripcion del utilizable
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Personaje que porta este utilizable
        /// </summary>
        public virtual ModeloPersonaje PersonajePortador { get; set; }
    }
}