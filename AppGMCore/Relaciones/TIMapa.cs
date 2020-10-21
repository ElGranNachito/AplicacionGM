using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    public class TIMapaVector2
    {
        [ForeignKey(nameof(Mapa))]
        public int IdMapa { get; set; }
        public ModeloMapa Mapa { get; set; }

        [ForeignKey(nameof(Posicion))]
        public int IdVector { get; set; }
        public ModeloVector2 Posicion { get; set; }
    }
}
