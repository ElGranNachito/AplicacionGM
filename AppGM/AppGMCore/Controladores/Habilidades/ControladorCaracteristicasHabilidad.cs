namespace AppGM.Core
{
    public class ControladorLimitador : ControladorBase<ModeloLimitador>
    {
        #region Miembros

        private int mUsosRestantes;
        private int mDiasRestantes;

        #endregion

        #region Constructor

        public ControladorLimitador(ModeloLimitador _modeloLimitador)
        {
            modelo = _modeloLimitador;
        }

        #endregion
    }

    public class ControladorCargasHabilidad : ControladorBase<ModeloCargasHabilidad>
    {
        #region Miembros

        private int CargasActuales;

        #endregion

        #region Constructor

        public ControladorCargasHabilidad(ModeloCargasHabilidad _modeloCargasHabilidad)
        {
            modelo = _modeloCargasHabilidad;
        }

        #endregion
    }
}