using System.Collections.Generic;

namespace AppGM.Core
{
    public class ModeloPortable : ModeloUtilizable
    {
        public List<TIPortableSlots> Slots { get; set; }

        //Primer indice son las ventajas, Segundo indice son las desventajas
        public List<TIPortableModificadorDeStatBase> VentajasYDesventajasDeEquiparlo { get; set; }
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
        public List<TIOfensivoTiradaDeDaño> TiradasDeDaño { get; set; }

        public int DañosQuePuedeInfligir { get; set; }
        
        public TIOfensivoEfecto EfectoQueInflige { get; set; }
    }
}