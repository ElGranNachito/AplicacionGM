namespace AppGMCore
{
    public abstract class ControladorUtilizable<TipoUtilizable> : IUtilizableConObjetivos, IUtilizableSinObjetivos
    {
        #region Eventos

        public delegate void dUtilizarHabilidad(ControladorHabilidad<ModeloHabilidad> habilidad, ControladorPersonaje<ModeloPersonaje> usuario, ControladorPersonaje<ModeloPersonaje>[] objetivos);

        public event dUtilizarHabilidad OnUtilizarHabilidad = delegate { };

        #endregion

        #region Funciones

        public virtual void Utilizar(ControladorPersonaje<ModeloPersonaje> usuario, ControladorPersonaje<ModeloPersonaje>[] objetivos, object parametroExtra, object segundoParametroExtra)
        {
            //TODO: Realizar la tirada de utilizacion. Verificar si le da al objetivo
        }

        public virtual void Utilizar(ControladorPersonaje<ModeloPersonaje> usuario, object parametroExtra, object segundoParametroExtra)
        {
            //TODO: Realizar la tirada de utilizacion.
        }

        #endregion
    }
}
