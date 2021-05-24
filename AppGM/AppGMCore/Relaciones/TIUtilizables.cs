using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    // Slot:

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloSlot"/> con un <see cref="ModeloItem"/> que almacena
    /// </summary>
    public class TISlotItem : ModeloBaseSK
    {
        [ForeignKey(nameof(Slot))]
        public int IdSlot { get; set; }
        public ModeloSlot Slot { get; set; }

        [ForeignKey(nameof(Item))]
        public int IdItem { get; set; }
        public ModeloItem Item { get; set; }
    }

    // Utilizables:

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloUtilizable"/>
    /// </summary>
    public abstract class TIUtilizable
    {
        [ForeignKey(nameof(Utilizable))]
        public int IdUtilizable { get; set; }
        public ModeloUtilizable Utilizable { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloUtilizable"/> con la <see cref="ModeloTiradaBase"/> que le corresponde
    /// </summary>
    public class TIUtilizableTiradaBase : TIUtilizable
    {
        [ForeignKey(nameof(TiradaBase))]
        public int IdTirada { get; set; }
        public ModeloTiradaBase TiradaBase { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloUtilizable"/> con el <see cref="ModeloModificadorDeStatBase"/> que tenga
    /// </summary>
    public class TIUtilizableModificadorDeStatBase : TIUtilizable
    {
        [ForeignKey(nameof(ModificadorDeStatBase))]
        public int IdModificadorStatBase { get; set; }
        public ModeloModificadorDeStatBase ModificadorDeStatBase { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloUtilizable"/> con el <see cref="ModeloEfecto"/> que aplique
    /// </summary>
    public class TIUtilizableEfecto : TIUtilizable
    {
        [ForeignKey(nameof(Efecto))]
        public int IdEfecto { get; set; }
        public ModeloEfecto Efecto { get; set; }
    }

    // Utilizables portables:

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloPortable"/>
    /// </summary>
    public abstract class TIPortable
    {
        [ForeignKey(nameof(Portable))]
        public int IdPortable { get; set; }
        public ModeloPortable Portable { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloPortable"/> con la <see cref="ModeloSlot"/> que aporte
    /// </summary>
    public class TIPortableSlots : TIPortable
    {
        [ForeignKey(nameof(Slot))]
        public int IdSlot { get; set; }
        public ModeloSlot Slot { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloPortable"/> con el <see cref="ModeloModificadorDeStatBase"/> que tenga
    /// </summary>
    public class TIPortableModificadorDeStatBase : TIPortable
    {
        [ForeignKey(nameof(Modificador))]
        public int IdModificadorDeStat { get; set; }
        public ModeloModificadorDeStatBase Modificador { get; set; }
    }

    // Utilizable portable ofensivo:

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloOfensivo"/>
    /// </summary>
    public abstract class TIOfensivo
    {
        [ForeignKey(nameof(Ofensivo))]
        public int IdOfensivo { get; set; }
        public ModeloOfensivo Ofensivo { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloOfensivo"/> con la <see cref="ModeloTiradaDeDaño"/> que le corresponda
    /// </summary>
    public class TIOfensivoTiradaDeDaño : TIOfensivo
    {
        [ForeignKey(nameof(TiradaDeDaño))]
        public int IdTiradaDeDaño { get; set; }
        public ModeloTiradaDeDaño TiradaDeDaño { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloOfensivo"/> con el <see cref="ModeloEfecto"/> que aplique
    /// </summary>
    public class TIOfensivoEfecto : TIOfensivo
    {
        [ForeignKey(nameof(Efecto))]
        public int IdEfecto { get; set; }
        public ModeloEfecto Efecto { get; set; }
    }

    // Consumible armas:

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloArmasDistancia"/>
    /// </summary>
    public abstract class TIArmasDistancia
    {
        [ForeignKey(nameof(ArmasDistancia))]
        public int IdArmasDistancia { get; set; }
        public ModeloArmasDistancia ArmasDistancia { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloArmasDistancia"/> con la <see cref="ModeloTiradaVariable"/> que le corresponda
    /// </summary>
    public class TIArmasDistanciaTiradaVariable : TIArmasDistancia
    {
        [ForeignKey(nameof(TiradaVariable))]
        public int IdTirada { get; set; }
        public ModeloTiradaVariable TiradaVariable { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloArmasDistancia"/> con la <see cref="ModeloTiradaDeDaño"/> que le corresponda
    /// </summary>
    public class TIArmasDistanciaTiradaDeDaño : TIArmasDistancia
    {
        [ForeignKey(nameof(TiradaDaño))]
        public int IdTirada { get; set; }
        public ModeloTiradaDeDaño TiradaDaño { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloArmasDistancia"/> con el <see cref="ModeloEfecto"/> que aplique
    /// </summary>
    public class TIArmasDistanciaEfecto : TIArmasDistancia
    {
        [ForeignKey(nameof(Efecto))]
        public int IdEfecto { get; set; }
        public ModeloEfecto Efecto { get; set; }
    }

}
