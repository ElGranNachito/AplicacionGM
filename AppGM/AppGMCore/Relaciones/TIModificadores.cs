namespace AppGM.Core
{
    public abstract class TIModificadorDeStatBase
    {
        public int IdModificadorDeStatBase { get; set; }
        public ModeloModificadorDeStatBase ModificadorDeStatBase { get; set; }
    }

    public class TIModificadorDeStatBaseTiradaBase : TIModificadorDeStatBase
    {
        public int IdTirada { get; set; }
        public ModeloTiradaBase TiradaBase { get; set; }
    }
}
