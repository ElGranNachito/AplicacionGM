using System.Collections.Generic;

namespace AppGM.Core
{
    public class ControladorPortable : ControladorUtilizable
    {
        #region Controladores

        public List<ControladorSlot> ControladorSlots { get; set; }

        // Portable ofensivo
        public List<ControladorTiradaDaño> ControladorTiradaDeDaño { get; set; }

        public ControladorEfecto ControladorEfectoQueInflige { get; set; }

        #endregion

        #region Constructor

        public ControladorPortable(ModeloPortable _modeloPortable)
			:base(_modeloPortable) {}

        #endregion

        #region Funciones

        public virtual void Equipar(ControladorPersonaje objetivo)
        {
            //TODO equipar item al objetivo.
        }

        public virtual void Desequipar(ControladorPersonaje objetivo)
        {
            //TODO desequipar item al objetivo.
        }

        #endregion
    }

    public class ControladorDefensivo : ControladorPortable
    { 
        public ControladorDefensivo(ModeloPortable _modelo)
            :base(_modelo)
		{

		}
    }
}