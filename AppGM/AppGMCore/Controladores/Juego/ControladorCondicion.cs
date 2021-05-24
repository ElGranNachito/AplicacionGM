using System;

namespace AppGM.Core
{
    public class ControladorCondicion : Controlador<ModeloCondicion>
    {
		#region Campos

		private Func<ControladorPersonaje, bool> mCondicion; 

		#endregion

		#region Constructor

		public ControladorCondicion(ModeloCondicion _modeloCondicion)
            :base(_modeloCondicion){}

        #endregion

        #region Funciones

        public bool SeCumpleLaCondicion(ControladorPersonaje pj)
        {
            return mCondicion(pj);
        }

        #endregion
    }
}