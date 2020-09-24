using System.Collections.Generic;

namespace AppGMCore
{
    public class ModeloHabilidad : IDescripcion
    {
        //Id
        public int IdHabilidad { get; set; }
        
        //Costos de mana que tiene la habilidad para ser utilizada
        public ushort CostoDeMana { get; set; }
        //Turnos que dura la habilidad
        public ushort TurnosDeDuracion { get; set; }
        
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public ModeloLimitador LimiteDeUsos { get; set; }
        private ControladorLimitador ControladorLimiteDeUso { get; set; }
        public ModeloCargasHabilidad CargasHabilidad { get; set; }
        private ControladorCargasHabilidad ControladorCargasHabilidad { get; set; }

        public ModeloTiradaDeDaño TiradaDeDaño { get; set; }
        private ControladorTiradaVariable<ModeloTiradaDeDaño> ControladorTiradaDeDaño { get; set; }
        
        public List<ModeloItem> ItemInvocacion { get; set; }
        private List<ControladorUtilizable<ModeloItem>> ControladorItemInvocacion { get; set; }
        public List<ModeloItem> ItemsQueCuesta { get; set; }
        private List<ControladorUtilizable<ModeloItem>> ControladorItemsQueCuesta { get; set; }
        
        public List<ModeloInvocacion> Invocación { get; set; }
        private List<ControladorInvocacion<ModeloInvocacion>> ControladorInvocacion { get; set; }
        
        public List<ModeloTiradaBase> TiradasDeUso { get; set; }
        private List<ControladorTiradaBase<ModeloTiradaBase>> ControladorTiradasDeUso { get; set; }

        public List<ModeloEfecto> EfectosSobreUsuario { get; set; }
        private List<ControladorEfecto<ModeloEfecto>> ControladorEfectosSobreUsuario { get; set; }
        public List<ModeloEfecto> EfectoSobreObjetivo { get; set; }
        private List<ControladorEfecto<ModeloEfecto>> ControladorEfectoSobreObjetivo { get; set; }
    }

    public class ModeloPerk : ModeloHabilidad
    {
        //Rango de la Perk
        public ERango Rango { get; set; }
    }

    public class ModeloMagia : ModeloHabilidad
    {
        public byte Nivel { get; set; }
    }

    public class ModeloNoblePhantasm : ModeloHabilidad
    {
        public ERango Rango { get; set; }
    }
}