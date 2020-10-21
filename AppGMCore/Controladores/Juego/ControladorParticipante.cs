namespace AppGM.Core
{
    public class ControladorParticipante : Controlador<ModeloParticipante>
    {
        #region Controladores

        private ControladorPersonaje mControladorPersonaje { get; set; }

        #endregion

        #region Constructores

        public ControladorParticipante(ModeloParticipante _modeloParticipante)
        {
            modelo = _modeloParticipante;

            mControladorPersonaje = new ControladorPersonaje(modelo.Personaje.Personaje);
        }

        #endregion

        #region Funciones

        //Nada de momento

        #endregion
    }
}
