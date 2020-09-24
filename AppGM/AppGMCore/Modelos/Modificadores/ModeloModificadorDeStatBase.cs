namespace AppGM.Core
{
    public class ModeloModificadorDeStatBase
    {
        //Id
        public int IdModificadorDeStat { get; set; }
        //Valor requerido para realizar tirada
        public int ValorRequeridoTirada { get; set; }

        //Valor de la stat en las tiradas
        public ModeloTiradaBase ValorTirada { get; set; }
        private ControladorTiradaBase<ModeloTiradaBase> ControaldorTiradaBase { get; set; }
    }

    public class ModeloModificadorDeStatPrimitivos : ModeloModificadorDeStatBase
    {
        //Stats afectadas por el modificador
        public int StatsQueAfecta { get; set; }

        //Valor aplicado a las stats
        public byte Valor { get; set; }
    }

    public class ModeloModificadorDeStatClase : ModeloModificadorDeStatBase
    {
        //Nombre de la clase a modificar
        public string NombreClase { get; set; }

        //Id de la clase
        public int IdObjeto { get; set; }
    }

    public class ModeloModificadorDeDefensa : ModeloModificadorDeStatBase
    {
        public int TiposDeDaño { get; set; }
        public int AlineamientosDelInstigador { get; set; }

        public byte ModificacionPorcentual { get; set; }
        public byte ModificacionFija { get; set; }
    }
}