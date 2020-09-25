using System.Collections.Generic;

namespace AppGM.Core
{
    public class DatosRol
    {
        public List<ControladorPersonaje<ModeloServant>> Servants { get; set; }
        public List<ControladorPersonaje<ModeloMaster>> Masters { get; set; }
        public List<ControladorInvocacion<ModeloInvocacion>> Invocaciones { get; set; }
        public List<ControladorUtilizable<ModeloItem>> Items { get; set; }
        public List<ControladorPortable<ModeloPortable>> Portables { get; set; }
        public List<ControladorPortable<ModeloOfensivo>> PortableOfensivo { get; set; }
        public List<ControladorDefensivo<ModeloDefensivo>> Defensivos { get; set; }
        public List<ControladorDefensivoAbsoluto> DefensivosAbsolutos { get; set; }
        public List<ControladorConsumible<ModeloConsumible>> Consumibles { get; set; }
        public List<ControladorArmaDistancia> ArmasDistancia { get; set; }
        public List<ControladorSlot> Slots { get; set; }
        
        public List<ControladorHabilidad<ModeloPerk>> Perks { get; set; }
        public List<ControladorHabilidad<ModeloHabilidad>> Skills { get; set; }
        public List<ControladorHabilidad<ModeloNoblePhantasm>> NoblePhantasms { get; set; }
        public List<ControladorMagia> Magias { get; set; }
         
        public List<ControladorEfecto<ModeloEfecto>> Efectos { get; set; }
        public List<ControladorCondicion> Condiciones { get; set; }
         
        public List<ControladorAdministradorDeCombate> CombatesActivos { get; set; }
         
        public List<ControladorLimitador> Limitadores { get; set; }
        public List<ControladorCargasHabilidad> CargasHabilidades { get; set; }
    }
}
