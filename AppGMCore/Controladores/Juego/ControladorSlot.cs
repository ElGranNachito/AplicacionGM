﻿using System.Collections.Generic;

namespace AppGM.Core
{
    public class ControladorSlot : Controlador<ModeloSlot>
    {
        #region Controladores

        public List<ControladorUtilizable> ControladorItemsAlmacenados { get; set; }

        #endregion
        
        #region Constructor

        public ControladorSlot(ModeloSlot _modeloSlot)
        {
            modelo = _modeloSlot;
        }

        #endregion

        #region Funciones

        public bool AlmacenarItem(ControladorUtilizable item)
        {
            //TODO: Chequear si queda espacio para almacenar dicho item y almacenarlo.
            //Retornar booleano indicando si se pudo almacenar el item.

            return false;
        }

        #endregion
    }
}