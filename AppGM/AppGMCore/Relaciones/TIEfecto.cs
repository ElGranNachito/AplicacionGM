namespace AppGM.Core
{
    public class TIEfectoModificadorDeStat
    {
        public int IdEfecto { get; set; }
        public ModeloEfecto Efecto { get; set; }

        public int IdModificadorDeStat { get; set; }
        public ModeloModificadorDeStatBase Modificador { get; set; }
    }
}
