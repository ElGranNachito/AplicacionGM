using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    //TODO: Decidir si es una clase abstracta o no
    public class ModeloUtilizable : ModeloBase
    {
        public ControladorUtilizable controladorUtilizable;

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
