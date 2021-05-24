namespace AppGM.Core
{
    public class ControladorLimitador : Controlador<ModeloLimitador>
    {
        #region Miembros
        
        #endregion

        #region Constructor

        public ControladorLimitador(ModeloLimitador _modeloLimitador)
			:base(_modeloLimitador) {}

        #endregion
    }

    public class ControladorCargasHabilidad : Controlador<ModeloCargasHabilidad>
    {
        #region Miembros

        #endregion

        #region Constructor

        public ControladorCargasHabilidad(ModeloCargasHabilidad _modeloCargasHabilidad)
			:base(_modeloCargasHabilidad) { }

        #endregion
    }
}