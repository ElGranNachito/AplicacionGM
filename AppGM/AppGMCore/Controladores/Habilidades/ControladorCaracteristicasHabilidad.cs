namespace AppGM.Core
{
    public class ControladorLimitador : Controlador<ModeloLimitador>
    {
        #region Miembros
        
        #endregion

        #region Constructor

        public ControladorLimitador(ModeloLimitador _modeloLimitador)
        {
            modelo = _modeloLimitador;
        }

        #endregion
    }

    public class ControladorCargasHabilidad : Controlador<ModeloCargasHabilidad>
    {
        #region Miembros

        #endregion

        #region Constructor

        public ControladorCargasHabilidad(ModeloCargasHabilidad _modeloCargasHabilidad)
        {
            modelo = _modeloCargasHabilidad;
        }

        #endregion
    }
}