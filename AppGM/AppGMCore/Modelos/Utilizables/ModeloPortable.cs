using System.Collections.Generic;

namespace AppGM.Core
{
    public class ModeloPortable : ModeloUtilizable
    {
        public List<ModeloSlot> Slots { get; set; }
        private List<ControladorSlot> controladorSlots { get; set; }

        public ModeloModificadorDeStatBase DesventajasDeEquiparlo { get; set; }
        private ControladorModificadorDeStatBase ControladorDesventajasDeEquiparlo { get; set; }
        public ModeloModificadorDeStatBase VentajasDeQuiparlo { get; set; }
        private ControladorModificadorDeStatBase ControladorVentajasDeEquiparlo { get; set; }
    }

    public class ModeloDefensivo : ModeloPortable
    {
        public EEstado Estado { get; set; }
    }

    public class ModeloDefensivoAbsoluto : ModeloDefensivo
    {
        private short mHP;
        
        public short Usos { get; set; }
    }

    public class ModeloOfensivo : ModeloPortable, IInfligeDaño
    {
        public List<ModeloTiradaDeDaño> TiradasDeDaño { get; set; }
        private List<ControladorTiradaBase<ModeloTiradaDeDaño>> ControladorTiradaDeDaño { get; set; }

        public int DañosQuePuedeInfligir { get; set; }
        
        public ModeloEfecto EfectoQueInflige { get; set; }
        private ControladorEfecto<ModeloEfecto> ControladorEfectoQueInflige { get; set; }
    }
}