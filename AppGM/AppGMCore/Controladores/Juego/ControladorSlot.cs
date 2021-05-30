using System.Collections.Generic;

namespace AppGM.Core
{
    /// <summary>
    /// Controlador de un <see cref="ModeloSlot"/>
    /// </summary>
    public class ControladorSlot : Controlador<ModeloSlot>
    {
        #region Controladores

        /// <summary>
        /// Lista con todos los <see cref="ControladorUtilizable"/> guardados
        /// </summary>
        public List<ControladorUtilizable> ControladorItemsAlmacenados { get; set; }

        #endregion
        
        #region Constructor

        public ControladorSlot(ModeloSlot _modeloSlot)
            :base(_modeloSlot){}

        #endregion

        #region Funciones

        /// <summary>
        /// Indica si puede cierto <see cref="ControladorUtilizable"/> puede ser almacenado por este slot
        /// </summary>
        /// <param name="item">Item que ver si puede ser almacenado</param>
        /// <returns><see cref="bool"/> indicando si este <paramref name="item"/> puede ser almacenado</returns>
        public bool PuedeAlmacenarItem(ControladorUtilizable item)
        {
            //TODO:
            //Devuelve un booleano indicando si cierto item puede ser almacenado por este slot

            return false;
        }

        /// <summary>
        /// Almacena un item
        /// </summary>
        /// <param name="item">Item a almacenar</param>
        /// <returns><see cref="bool"/> indicando si el item fue almacenado exitosamente</returns>
        public bool AlmacenarItem(ControladorUtilizable item)
        {
	        if (!PuedeAlmacenarItem(item))
		        return false;

            return false;
        }

        #endregion
    }
}
