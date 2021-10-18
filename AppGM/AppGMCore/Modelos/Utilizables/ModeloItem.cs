using System.Collections.Generic;

namespace AppGM.Core
{
    public class ModeloItem : ModeloUtilizable
    {
        /// <summary>
        /// Slots que ocupa el item
        /// </summary>
        public decimal SlotsQueOcupa { get; set; }
    }

    public class ModeloConsumible : ModeloItem
    {
        /// <summary>
        /// Cantidad de usos que maximos que puede tener el consumible
        /// </summary>
        public ushort Usos { get; set; }
        ///Cantidad de usos que le quedan al consumible
        public ushort UsosRestantes { get; set; }
    }

    public class ModeloArmasDistancia : ModeloConsumible, IInfligeDaño
    {
    }
}
