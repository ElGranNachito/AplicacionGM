using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    /// <summary>
    /// Representa una relacion de una <see cref="ModeloAlianza"/> con el <see cref="ModeloContrato"/> que incluya
    /// </summary>
    public class TIAlianzaContrato : ModeloBaseSK
    {
        [ForeignKey(nameof(Alianza))]
        public int IdAlianza { get; set; }
        public ModeloAlianza Alianza { get; set; }

        [ForeignKey(nameof(Contrato))]
        public int IdContrato { get; set; }
        public ModeloContrato Contrato { get; set; }
    }
}
