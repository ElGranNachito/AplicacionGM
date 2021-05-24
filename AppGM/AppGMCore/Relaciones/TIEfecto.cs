using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    /// <summary>
    /// Representa una relacion de un <see cref="ModeloEfecto"/> con el <see cref="ModeloModificadorDeStatBase"/> que tenga
    /// </summary>
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
