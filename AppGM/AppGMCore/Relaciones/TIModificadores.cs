using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    public abstract class TIModificadorDeStatBase : ModeloBaseSK
    {
        [ForeignKey(nameof(ModificadorDeStatBase))]
        public int IdModificadorDeStatBase { get; set; }
        public ModeloModificadorDeStatBase ModificadorDeStatBase { get; set; }
    }

    public class TIModificadorDeStatBaseTiradaBase : TIModificadorDeStatBase
    {
        [ForeignKey(nameof(TiradaBase))]
        public int IdTirada { get; set; }
        public ModeloTiradaBase TiradaBase { get; set; }
    }
}
