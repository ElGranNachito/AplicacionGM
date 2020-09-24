namespace AppGMCore
{
    abstract class ControladorTiradaBase<TipoTirada>
    {
        #region Propiedades

        public int Resultado { get; set; }

        #endregion

        #region Funciones

        public virtual void RealizarTirada()
        {
        }

        #endregion
    }

    class ControladorTiradaVariable<TipoVariable> : ControladorTiradaBase<ModeloTiradaVariable>
    {
    }

    class ControladorTiradaStat<TipoStat> : ControladorTiradaBase<ModeloTiradaStat>
    {
        #region Funciones

        public void RealizarTirada(ControladorPersonaje<ModeloPersonaje> p)
        {
            //TODO: Tirar 3d6
        }

        #endregion
    }
}