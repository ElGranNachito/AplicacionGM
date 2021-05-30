namespace AppGM.Core
{
    /// <summary>
    /// Controlador de un <see cref="ModeloLimitador"/>
    /// </summary>
    public class ControladorLimitador : Controlador<ModeloLimitador>
    {
        #region Miembros
        
        #endregion

        #region Constructor

        public ControladorLimitador(ModeloLimitador _modeloLimitador)
			:base(_modeloLimitador) {}

        #endregion
    }

    /// <summary>
    /// Controlador de <see cref="ModeloCargas"/>
    /// </summary>
    public class ControladorCargasHabilidad : Controlador<ModeloCargas>
    {
        #region Miembros

        #endregion

        #region Constructor

        public ControladorCargasHabilidad(ModeloCargas modeloCargas)
			:base(modeloCargas) { }

        #endregion
    }
}