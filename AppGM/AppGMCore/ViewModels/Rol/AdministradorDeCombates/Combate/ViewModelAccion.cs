namespace AppGM.Core
{
    /// <summary>
    /// VM que representa un <see cref="ModeloAccion"/>
    /// </summary>
    public class ViewModelAccion : ViewModel
    {
        #region Miembros

        // Campos ---


        /// <summary>
        /// Controlador de esta accion.
        /// </summary>
        public ControladorAccion accion;

        /// <summary>
        /// VM del participante.
        /// </summary>
        public ViewModelParticipante participante;


        // Propiedades ---


        /// <summary>
        /// Nombre del participante que realizo la accion.
        /// </summary>
        public string NombreParticipante => accion.modelo.Participante.Personaje.Nombre;

        /// <summary>
        /// Descripcion de la accion.
        /// </summary>
        public string Descripcion => accion.modelo.Descripcion;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="_accion">Controlador de la accion</param>
        public ViewModelAccion(ControladorAccion _accion, ViewModelParticipante _participante)
        {
            accion = _accion;
            participante = _participante;
        }

        #endregion

        #region Funciones

        

        #endregion
    }
}
