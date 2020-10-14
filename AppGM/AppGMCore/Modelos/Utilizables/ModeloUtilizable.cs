using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    //TODO: Decidir si es abstracta o no
    public class ModeloUtilizable
    {
        //Id
        [Key]
        public int IdUtilizable { get; set; }

        //Stats
        public EStat EStatQueAfecta { get; set; }
        public EStat EStatDeLaQueDepende { get; set; }

        //Tirada requerida para poder se utilizado
        public TIUtilizableTiradaBase TiradaDeUso { get; set; }

        //Modificador para la stat afectada por el utilizable
        public TIUtilizableModificadorDeStatBase VentajaAlUtilizarlo { get; set; }

        //Efectos al utilizarlo
        public TIUtilizableEfecto EfectoSobreElUsuario { get; set; }
        
        public TIUtilizableEfecto EfectoSobreElObjetivo { get; set; }
    }
}
