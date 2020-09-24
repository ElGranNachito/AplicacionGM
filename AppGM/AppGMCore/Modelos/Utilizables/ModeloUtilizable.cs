namespace AppGMCore
{
    public abstract class ModeloUtilizable
    {
        //Id
        public int IdUtilizable { get; set; }

        //Stats
        public EStat EStatQueAfecta { get; set; }
        public EStat EStatDeLaQueDepende { get; set; }

        //Tirada requerida para poder se utilizado
        public ModeloTiradaBase TiradaDeUso { get; set; }
        private ControladorTiradaBase<ModeloTiradaBase> ControladorTiradaDeUso { get; set; }

        //Modificador para la stat afectada por el utilizable
        public ModeloModificadorDeStatBase VentajaAlUtilizarlo { get; set; }
        private ControladorModificadorDeStatBase ControladorVentajaAlUtilizarlo { get; set; }

        //Efectos al utilizarlo
        public ModeloEfecto EfectoSobreElUsuario { get; set; }
        ControladorEfecto<ModeloEfecto> controladorEfectoSobreElUsuario { get; set; }
        
        public ModeloEfecto EfectoSobreElObjetivo { get; set; }
        ControladorEfecto<ModeloEfecto> controladorEfectoSobreElObjetivo { get; set; }
    }
}
