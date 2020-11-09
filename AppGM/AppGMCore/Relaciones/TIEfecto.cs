using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    public class TIEfectoModificadorDeStatBase : ModeloBaseSK
    {
        [ForeignKey(nameof(Efecto))]
        public int IdEfecto { get; set; }
        public ModeloEfecto Efecto { get; set; }

        [ForeignKey(nameof(Modificador))]
        public int IdModificadorDeStat { get; set; }
        public ModeloModificadorDeStatBase Modificador { get; set; }
    }
}
