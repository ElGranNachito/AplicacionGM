using System.Collections.Generic;

namespace AppGM.Core
{
    public class ModeloItem : ModeloUtilizable
    {
        public decimal StatsQueOcupa { get; set; }
    }

    public class ModeloConsumible : ModeloItem
    {
        public ushort Usos { get; set; }
        public ushort UsosRestantes { get; set; }
    }

    public class ModeloArmasDistancia : ModeloConsumible, IInfligeDaño
    {
        public ModeloTiradaDeDaño TiradaDeDaño { get; set; }
        private ControladorTiradaVariable<ModeloTiradaDeDaño> ControladorTiradaDeDaño { get; set; }

        public ModeloTiradaVariable TiradaRafaga { get; set; }
        private ControladorTiradaVariable<ModeloTiradaVariable> ControladorTiradaVariable { get; set; }

        public ETipoDeDaño TipoDeDañoQueInflige { get; set; }
        
        List<ModeloEfecto> EfectoQueInflige { get; set; }
        List<ControladorEfecto<ModeloEfecto>> ControladorEfectoQueInflige { get; set; }
    }
}
