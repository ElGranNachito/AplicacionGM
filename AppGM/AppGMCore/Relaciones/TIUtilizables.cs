using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    // Slot
    public class TISlotItem : ModeloBaseSK
    {
        [ForeignKey(nameof(Slot))]
        public int IdSlot { get; set; }
        public ModeloSlot Slot { get; set; }

        [ForeignKey(nameof(Item))]
        public int IdItem { get; set; }
        public ModeloItem Item { get; set; }
    }

    // Utilizables 
    public abstract class TIUtilizable
    {
        [ForeignKey(nameof(Utilizable))]
        public int IdUtilizable { get; set; }
        public ModeloUtilizable Utilizable { get; set; }
    }

    public class TIUtilizableTiradaBase : TIUtilizable
    {
        [ForeignKey(nameof(TiradaBase))]
        public int IdTirada { get; set; }
        public ModeloTiradaBase TiradaBase { get; set; }
    }

    public class TIUtilizableModificadorDeStatBase : TIUtilizable
    {
        [ForeignKey(nameof(ModificadorDeStatBase))]
        public int IdModificadorStatBase { get; set; }
        public ModeloModificadorDeStatBase ModificadorDeStatBase { get; set; }
    }

    public class TIUtilizableEfecto : TIUtilizable
    {
        [ForeignKey(nameof(Efecto))]
        public int IdEfecto { get; set; }
        public ModeloEfecto Efecto { get; set; }
    }

    // Utilizables portables
    public abstract class TIPortable
    {
        [ForeignKey(nameof(Portable))]
        public int IdPortable { get; set; }
        public ModeloPortable Portable { get; set; }
    }

    public class TIPortableSlots : TIPortable
    {
        [ForeignKey(nameof(Slot))]
        public int IdSlot { get; set; }
        public ModeloSlot Slot { get; set; }
    }

    public class TIPortableModificadorDeStatBase : TIPortable
    {
        [ForeignKey(nameof(Modificador))]
        public int IdModificadorDeStat { get; set; }
        public ModeloModificadorDeStatBase Modificador { get; set; }
    }

    // Utilizable portable ofensivo
    public abstract class TIOfensivo
    {
        [ForeignKey(nameof(Ofensivo))]
        public int IdOfensivo { get; set; }
        public ModeloOfensivo Ofensivo { get; set; }
    }

    public class TIOfensivoTiradaDeDaño : TIOfensivo
    {
        [ForeignKey(nameof(TiradaDeDaño))]
        public int IdTiradaDeDaño { get; set; }
        public ModeloTiradaDeDaño TiradaDeDaño { get; set; }
    }

    public class TIOfensivoEfecto : TIOfensivo
    {
        [ForeignKey(nameof(Efecto))]
        public int IdEfecto { get; set; }
        public ModeloEfecto Efecto { get; set; }
    }

    // Consumible armas
    public abstract class TIArmasDistancia
    {
        [ForeignKey(nameof(ArmasDistancia))]
        public int IdArmasDistancia { get; set; }
        public ModeloArmasDistancia ArmasDistancia { get; set; }
    }

    public class TIArmasDistanciaTiradaVariable : TIArmasDistancia
    {
        [ForeignKey(nameof(TiradaVariable))]
        public int IdTirada { get; set; }
        public ModeloTiradaVariable TiradaVariable { get; set; }
    }

    public class TIArmasDistanciaTiradaDeDaño : TIArmasDistancia
    {
        [ForeignKey(nameof(TiradaDaño))]
        public int IdTirada { get; set; }
        public ModeloTiradaDeDaño TiradaDaño { get; set; }
    }

    public class TIArmasDistanciaEfecto : TIArmasDistancia
    {
        [ForeignKey(nameof(Efecto))]
        public int IdEfecto { get; set; }
        public ModeloEfecto Efecto { get; set; }
    }

}
