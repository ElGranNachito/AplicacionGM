namespace AppGM.Core
{
    public class ControladorParticipante : ControladorBase<ModeloParticipante>
    {
        #region Controladores

        private ControladorPersonaje<ModeloPersonaje> mControladorPersonaje { get; set; }

        #endregion

        #region Constructores

        public ControladorParticipante(ModeloParticipante _modeloParticipante)
        {
            modelo = _modeloParticipante;

            mControladorPersonaje = new ControladorPersonaje<ModeloPersonaje>(modelo.Personaje.Personaje);
        }

        #endregion

        #region Constructor

        public ControladorParticipante(ModeloParticipante _modeloParticipante)
        {
            modelo = _modeloParticipante;
        }

        #endregion

        #region Funciones

        //Nada de momento

        #endregion
    }
}
