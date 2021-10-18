namespace AppGM.Core
{
    /// <summary>
    /// Controlador de un <see cref="ModeloParticipante"/>
    /// </summary>
    public class ControladorParticipante : Controlador<ModeloParticipante>
    {
        #region Controladores

        /// <summary>
        /// Controlador del personaje que representa este <see cref="ModeloParticipante"/>
        /// </summary>
        public ControladorPersonaje ControladorPersonaje { get; private set; }

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_modeloParticipante">Modleo del participante</param>
        public ControladorParticipante(ModeloParticipante _modeloParticipante)
			:base(_modeloParticipante)
        {
	        ControladorPersonaje = SistemaPrincipal.ObtenerControlador<ControladorPersonaje, ModeloPersonaje>(modelo.Personaje, true);
        }

        #endregion

        #region Funciones

        //Nada de momento

        #endregion
    }
}
