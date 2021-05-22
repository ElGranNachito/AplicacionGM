using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public class ModeloSlot : ModeloBase
    {
        public ControladorSlot controladorSlot;

        //Espacio total en la slot
        public decimal EspacioTotal      { get; set; }
        //Espacio disponible en la slot
        public decimal EspacioDisponible { get; set; }

        //Items almacenados en la slot
        public List<TISlotItem> ItemsAlmacenados { get; set; } = new List<TISlotItem>();
    }
}
