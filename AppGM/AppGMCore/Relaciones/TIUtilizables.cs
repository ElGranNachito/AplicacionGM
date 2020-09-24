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
        private int IdTirada { get; set; }
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
        public int idSlot { get; set; }
        public ModeloSlot Slot { get; set; }
    }

    public class TIPortableEfecto : TIPortable
    {
        public int idEfecto { get; set; }
        public ModeloEfecto Efecto { get; set; }
    }
    
    public class TIPortableModificadorDeStat : TIPortable
    {
        public int idModificadorDeStat { get; set; }
        public ModeloModificadorDeStatBase Modificador { get; set; }
    }

    // Portable armas
    public abstract class TIArmasDistancia
    {
        public int idArmasDistancia { get; set; }
        public ModeloArmasDistancia ArmasDistancia { get; set; }
    }

    public class TIArmasDistanciaTirada : TIArmasDistancia
    {
        public int IdTirada { get; set; }
        public ModeloTiradaVariable TiradaVariable { get; set; }
    }

}
