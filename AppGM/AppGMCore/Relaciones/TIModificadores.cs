using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    /// <summary>
    /// Representa una relacion de un <see cref="ModeloModificadorDeStatBase"/>
    /// </summary>
    public abstract class TIModificadorDeStatBase : ModeloBaseSK
    {
        [ForeignKey(nameof(ModificadorDeStatBase))]
        public int IdModificadorDeStatBase { get; set; }
        public ModeloModificadorDeStatBase ModificadorDeStatBase { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloModificadorDeStatBase"/> con la <see cref="ModeloTiradaBase"/> que le corresponda
    /// </summary>
    public class TIModificadorDeStatBaseTiradaBase : TIModificadorDeStatBase
    {
        [ForeignKey(nameof(TiradaBase))]
        public int IdTirada { get; set; }
        public ModeloTiradaBase TiradaBase { get; set; }
    }
}
