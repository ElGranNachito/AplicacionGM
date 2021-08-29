using System.Collections.Generic;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de datos para el utilizable
    /// </summary>
    public class ModeloUtilizable : ModeloConVariablesYTiradas<TIVariableUtilizable, TITiradaUtilizable>
    {
	    /// <summary>
        /// Peso del utilizable
        /// </summary>
        public decimal Peso { get; set; }

        /// <summary>
        /// Stats afectadas por el utilizable
        /// </summary>
        public EStat EStatQueAfecta { get; set; }
        /// <summary>
        /// Stats de las que depende el utilizable
        /// </summary>
        public EStat EStatDeLaQueDepende { get; set; }

        /// <summary>
        /// Tirada requerida para poder se utilizado
        /// </summary>
        public virtual TIUtilizableTiradaBase TiradaDeUso { get; set; }

        /// <summary>
        /// Modificador para la stat afectada por el utilizable
        /// </summary>
        public virtual TIUtilizableModificadorDeStatBase VentajaAlUtilizarlo { get; set; }

        /// <summary>
        /// Efectos al utilizarlo - Primer indice efecto sobre el usuario, Segundo indice efecto sobre el objetivo
        /// </summary>
        public virtual List<TIUtilizableEfecto> EfectoSobreUsuarioYObjetivo { get; set; } = new List<TIUtilizableEfecto>();
    }
}
