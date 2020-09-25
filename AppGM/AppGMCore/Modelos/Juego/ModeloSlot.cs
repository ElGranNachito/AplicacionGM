using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public class ModeloSlot
    {
        //Id
        [Key]
        public int IdSlot { get; set; }

        //Espacio en la slot
        public decimal Espacio { get; set; }

        //Items almacenados en la slot
        public List<ModeloItem> ItemsAlmacenados { get; set; }
        private List<ControladorUtilizable<ModeloItem>> ControladorItemsAlmacenados { get; set; }
    }
}
