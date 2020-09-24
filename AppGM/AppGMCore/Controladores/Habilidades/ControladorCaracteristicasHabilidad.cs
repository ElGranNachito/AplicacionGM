namespace AppGM.Core
{
    class ControladorLimitador : ControladorBase<ModeloLimitador>
    {
        #region Miembros

        private int mUsosRestantes;
        private int mDiasRestantes;

        #endregion
    }

    class ControladorCargasHabilidad : ControladorBase<ModeloCargasHabilidad>
    {
        #region Miembros

        private int CargasActuales;

        #endregion
    }
}