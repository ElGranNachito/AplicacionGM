using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public class ModeloUtilizable : ModeloBase
    {
        public ControladorUtilizable controladorUtilizable;

        //Peso del utilizable
        public decimal Peso { get; set; }

        //Stats
        public EStat EStatQueAfecta { get; set; }
        public EStat EStatDeLaQueDepende { get; set; }

        //Tirada requerida para poder se utilizado
        public TIUtilizableTiradaBase TiradaDeUso { get; set; }

        //Modificador para la stat afectada por el utilizable
        public TIUtilizableModificadorDeStatBase VentajaAlUtilizarlo { get; set; }

        //Efectos al utilizarlo - Primer indice efecto sobre el usuario, Segundo indice efecto sobre el objetivo
        public List<TIUtilizableEfecto> EfectoSobreUsuarioYObjetivo { get; set; } = new List<TIUtilizableEfecto>();
    }
}
