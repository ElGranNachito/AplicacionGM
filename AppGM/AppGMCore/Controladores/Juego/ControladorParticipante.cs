namespace AppGM.Core
{
    public class ControladorParticipante : ControladorBase<ModeloParticipante>
    {
        #region Controladores

        public ControladorPersonaje<ModeloPersonaje> ControladorPersonaje { get; set; }

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
