using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public class ModeloModificadorDeStatBase : ModeloBase
    {
        //Valor requerido para realizar tirada
        public int ValorRequeridoTirada { get; set; }

        //Valor de la stat en las tiradas
        public TIModificadorDeStatBaseTiradaBase ValorTirada { get; set; }
    }

    public class ModeloModificadorDeStatPrimitivos : ModeloModificadorDeStatBase
    {
        //Stats afectadas por el modificador. Terner en cuenta los valores de EStat
        public int StatsQueAfecta { get; set; }

        //Valor aplicado a las stats
        public byte Valor { get; set; }
    }

    public class ModeloModificadorDeStatClase : ModeloModificadorDeStatBase
    {
        //Nombre de la clase a modificar
        [StringLength(50)]
        public string NombreClase { get; set; }

        //Id de la clase
        public int IdObjeto { get; set; }
    }

    public class ModeloModificadorDeDefensa : ModeloModificadorDeStatBase
    {
        //Tener en cuenta los valores de ETipoDeDaño
        public int TiposDeDaño { get; set; }
        //Tener en cuenta los valores de EAlineamiento
        public int AlineamientosDelInstigador { get; set; }

        public byte ModificacionPorcentual { get; set; }
        public byte ModificacionFija { get; set; }
    }
}