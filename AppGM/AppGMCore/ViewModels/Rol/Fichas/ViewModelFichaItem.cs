using AppGM.Core;

namespace AppGM
{

    /// <summary>
    /// View model para un item en un ItemControl que contiene datos basicos de una ficha de un personaje
    /// </summary>
    public class ViewModelFichaItem : ViewModel
    {
        #region Miembros

        // Campos ---


        private ControladorPersonaje personaje;


        // Propiedades ---

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_personaje">Personaje que representa esta ficha</param>
        public ViewModelFichaItem(ControladorPersonaje _personaje)
        {
            personaje = _personaje;
        }

        #endregion

        #region Funciones

        

        #endregion
    }
}
