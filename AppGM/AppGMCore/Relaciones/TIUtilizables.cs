namespace AppGM.Core
{
    // Slot
    public class TISlotItem
    {
        public int IdSlot { get; set; }
        public ModeloSlot Slot { get; set; }

        public int IdItem { get; set; }
        public ModeloItem Item { get; set; }
    }

    // Utilizables 
    public abstract class TIUtilizable
    {
        public int IdUtilizable { get; set; }
        public ModeloUtilizable Utilizable { get; set; }
    }

    public class TIUtilizableTiradaBase : TIUtilizable
    {
        public int IdTirada { get; set; }
        public ModeloTiradaBase TiradaBase { get; set; }
    }

    public class TIUtilizableModificadorDeStatBase : TIUtilizable
    {
        public int IdModificadorStatBase { get; set; }
        public ModeloModificadorDeStatBase ModificadorDeStatBase { get; set; }
    }

    public class TIUtilizableEfecto : TIUtilizable
    {
        public int IdEfecto { get; set; }
        public ModeloEfecto Efecto { get; set; }
    }

    // Utilizables portables
    public abstract class TIPortable
    {
        public int IdPortable { get; set; }
        public ModeloPortable Portable { get; set; }
    }

    public class TIPortableSlots : TIPortable
    {
        public int IdSlot { get; set; }
        public ModeloSlot Slot { get; set; }
    }

    public class TIPortableModificadorDeStatBase : TIPortable
    {
        public int IdModificadorDeStat { get; set; }
        public ModeloModificadorDeStatBase Modificador { get; set; }
    }

    // Utilizable portable ofensivo
    public abstract class TIOfensivo
    {
        public int IdOfensivo { get; set; }
        public ModeloOfensivo Ofensivo { get; set; }
    }

    public class TIOfensivoTiradaDeDaño : TIOfensivo
    {
        public int IdTiradaDeDaño { get; set; }
        public ModeloTiradaDeDaño TiradaDeDaño { get; set; }
    }

    public class TIOfensivoEfecto : TIOfensivo
    {
        public int IdEfecto { get; set; }
        public ModeloEfecto Efecto { get; set; }
    }

    // Consumible armas
    public abstract class TIArmasDistancia
    {
        public int IdArmasDistancia { get; set; }
        public ModeloArmasDistancia ArmasDistancia { get; set; }
    }

    public class TIArmasDistanciaTiradaVariable : TIArmasDistancia
    {
        public int IdTirada { get; set; }
        public ModeloTiradaVariable TiradaVariable { get; set; }
    }

    public class TIArmasDistanciaTiradaDeDaño : TIArmasDistancia
    {
        public int IdTirada { get; set; }
        public ModeloTiradaDeDaño TiradaDaño { get; set; }
    }

    public class TIArmasDistanciaEfecto : TIArmasDistancia
    {
        public int IdEfecto { get; set; }
        public ModeloEfecto Efecto { get; set; }
    }

}
