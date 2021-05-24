using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    /// <summary>
    /// Representa una relacion de un <see cref="ModeloMapa"/>
    /// </summary>
    public class TIMapa : ModeloBaseSK
    {
        [ForeignKey(nameof(Mapa))]
        public int IdMapa { get; set; }
        public ModeloMapa Mapa { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloMapa"/> con la <see cref="ModeloUnidadMapa"/> que abarque
    /// </summary>
    public class TIMapaUnidadMapa : TIMapa
    {
        [ForeignKey(nameof(Unidad))]
        public int IdUnidadMapa { get; set; }
        public ModeloUnidadMapa Unidad { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloUnidadMapa"/> con la <see cref="ModeloVector2"/> que represente
    /// </summary>
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
