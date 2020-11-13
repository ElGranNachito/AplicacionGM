using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    public class TIMapa : ModeloBaseSK
    {
        [ForeignKey(nameof(Mapa))]
        public int IdMapa { get; set; }
        public ModeloMapa Mapa { get; set; }
    }
    public class TIMapaUnidadMapa : TIMapa
    {
        [ForeignKey(nameof(Unidad))]
        public int IdUnidadMapa { get; set; }
        public ModeloUnidadMapa Unidad { get; set; }
    }

    public class TIUnidadMapaVector2 : ModeloBaseSK
    {
        [ForeignKey(nameof(Unidad))]
        public int IdUnidadMapa { get; set; }
        public ModeloUnidadMapa Unidad { get; set; }

        [ForeignKey(nameof(Posicion))]
        public int IdVector { get; set; }
        public ModeloVector2 Posicion { get; set; }
    }


}
