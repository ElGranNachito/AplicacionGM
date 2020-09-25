namespace AppGM.Core
{
    public class ControladorLimitador : ControladorBase<ModeloLimitador>
    {
        #region Miembros

        private int mUsosRestantes;
        private int mDiasRestantes;

        #endregion
    }

    public class ControladorCargasHabilidad : ControladorBase<ModeloCargasHabilidad>
    {
        #region Miembros

        private int CargasActuales;

        #endregion
    }
}