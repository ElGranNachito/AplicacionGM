using System.Collections.Generic;

namespace AppGM.Core
{
    public class ModeloPortable : ModeloUtilizable
    {
        public ControladorPortable controladorPortable;

        /// <summary>
        /// Condicion porcentual del portable
        /// </summary>
        public int Estado { get; set; }

        /// <summary>
        /// Slots que aporta el portable
        /// </summary>
        public virtual List<ModeloSlot> Slots { get; set; }
    }

    public class ModeloDefensivo : ModeloPortable
	{

	}
}