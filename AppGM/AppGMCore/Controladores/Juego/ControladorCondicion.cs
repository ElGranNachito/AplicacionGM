using System;

namespace AppGM.Core
{
    /// <summary>
    /// Controlador de una <see cref="ModeloCondicion"/>.
    /// Clase encargada de albergar la funcion que representa esta condicion
    /// </summary>
    public class ControladorCondicion : Controlador<ModeloCondicion>
    {
		#region Campos

		private Func<ControladorPersonaje, bool> mCondicion; 

		#endregion

		#region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_modeloCondicion">Modelo de la condicion</param>
		public ControladorCondicion(ModeloCondicion _modeloCondicion)
            :base(_modeloCondicion){}

        #endregion

        #region Funciones

        /// <summary>
        /// Revisa si se cumple la condicion para determinado personaje
        /// </summary>
        /// <param name="pj">Personaje sobre el cual evaluar la condicion</param>
        /// <returns><see cref="bool"/> indicando si la condicion se cumple</returns>
        public bool SeCumpleLaCondicion(ControladorPersonaje pj) => mCondicion(pj);

        #endregion
    }
}