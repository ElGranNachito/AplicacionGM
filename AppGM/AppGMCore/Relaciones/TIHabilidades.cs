using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    /// <summary>
    /// Representa una relacion de un <see cref="ModeloHabilidad"/>
    /// </summary>
    public abstract class TIHabilidad : ModeloBaseSK
    {
        [ForeignKey(nameof(Habilidad))]
        public int IdHabilidad { get; set; }

        public ModeloHabilidad Habilidad { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloHabilidad"/> con el <see cref="ModeloLimitador"/> que tenga
    /// </summary>
    public class TIHabilidadLimitador : TIHabilidad
    {
        [ForeignKey(nameof(ModeloLimitador))]
        public int IdLimitador { get; set; }
        public ModeloLimitador ModeloLimitador { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloHabilidad"/> con el <see cref="ModeloCargas"/> que tenga
    /// </summary>
    public class TIHabilidadCargasHabilidad : TIHabilidad
    {
        [ForeignKey(nameof(ModeloCargas))]
        public int IdCargasHabilidad { get; set; }
        public ModeloCargas ModeloCargas { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloHabilidad"/> con la <see cref="ModeloTiradaBase"/> que le corresponda
    /// </summary>
    public class TIHabilidadTiradaBase : TIHabilidad
    {
        [ForeignKey(nameof(TiradaBase))]
        public int IdTirada { get; set; }
        public ModeloTiradaBase TiradaBase { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloHabilidad"/> con la <see cref="ModeloTiradaDeDaño"/> que le corresponda
    /// </summary>
    public class TIHabilidadTiradaDeDaño : TIHabilidad
    {
        [ForeignKey(nameof(TiradaDeDaño))]
        public int IdTirada { get; set; }
        public ModeloTiradaDeDaño TiradaDeDaño { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloHabilidad"/> con el <see cref="ModeloItem"/> que utilice
    /// </summary>
    public class TIHabilidadItem : TIHabilidad
    {
        [ForeignKey(nameof(Item))]
        public int IdItem { get; set; }
        public ModeloItem Item { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloHabilidad"/> con la <see cref="ModeloInvocacion"/> que involucre
    /// </summary>
    public class TIHabilidadInvocacion : TIHabilidad
    {
        [ForeignKey(nameof(Invocacion))]
        public int IdInvocacion { get; set; }

        public ModeloInvocacion Invocacion { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloHabilidad"/> con el <see cref="ModeloEfecto"/> que aplique
    /// </summary>
    public class TIHabilidadEfecto : TIHabilidad
    {
        [ForeignKey(nameof(Efecto))]
        public int IdEfecto { get; set; }
        public ModeloEfecto Efecto { get; set; }
    }
}
