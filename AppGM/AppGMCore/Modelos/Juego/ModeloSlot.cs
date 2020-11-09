using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public class ModeloSlot : ModeloBase
    {
        //Espacio en la slot
        public decimal Espacio { get; set; }

        //Items almacenados en la slot
        public List<TISlotItem> ItemsAlmacenados { get; set; } = new List<TISlotItem>();
    }
}
