using System.Collections.Generic;

namespace AppGM.Core
{
    public class ControladorSlot : ControladorBase<ModeloSlot>
    {
        #region Controladores

        public List<ControladorUtilizable<ModeloItem>> ControladorItemsAlmacenados { get; set; }

        #endregion

        #region Funciones

        public bool AlmacenarItem(ControladorUtilizable<ModeloItem> item)
        {
            //TODO: Chequear si queda espacio para almacenar dicho item y almacenarlo.
            //Retornar booleano indicando si se pudo almacenar el item.

            return false;
        }

        #endregion
    }
}
