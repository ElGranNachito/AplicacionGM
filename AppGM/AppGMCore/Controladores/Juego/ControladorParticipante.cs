namespace AppGM.Core
{
    public class ControladorParticipante : Controlador<ModeloParticipante>
    {
        #region Controladores

        public ControladorPersonaje ControladorPersonaje { get; private set; }

        #endregion

        #region Constructores

        public ControladorParticipante(ModeloParticipante _modeloParticipante)
        {
            modelo = _modeloParticipante;

            ControladorPersonaje = modelo.Personaje.Personaje.controlador;
        }

        #endregion

        #region Funciones

        //Nada de momento

        #endregion
    }
}
