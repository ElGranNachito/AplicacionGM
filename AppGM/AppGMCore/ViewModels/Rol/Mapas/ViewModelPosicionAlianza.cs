using System.ComponentModel;
using System.IO;

namespace AppGM.Core
{
    /// <summary>
    /// View model que representa al identificador de ciertas alianzas que tiene un personaje.
    /// El identificador se visualiza como una pequeña insignea sobre la unidad del personaje visualizado sobre el mapa.
    /// </summary>
    public class ViewModelPosicionAlianza : ViewModel
    {
        #region Campos & Propiedades


        // -------------------------CAMPOS----------------------------


        public readonly ControladorAlianza controladorAlianza;


        // -----------------------PROPIEDADES----------------------------------

        /// <summary>
        /// Ruta de la imagen de la unidad
        /// </summary>
        public string PathImagenIcono => Path.Combine(SistemaPrincipal.ControladorDeArchivos.DirectorioImagenes, "Iconos/Alianzas/Team_UwU.png");


        #endregion

        #region Constructores

        /// <summary>
        /// Constrcutor
        /// </summary>
        /// <param name="_mapa">Viewmodel del mapa</param>
        /// <param name="_unidad">Contralador de la unidad</param>
        public ViewModelPosicionAlianza(ControladorAlianza alianza)
        {
            controladorAlianza = alianza;

            DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PathImagenIcono)));
        }

        #endregion

        #region Funciones



        #endregion
    }
}
