namespace AppGM.Core
{
    //TODO: Decidir si es abstracta o no
    public class ControladorUtilizable<TipoUtilizable> : ControladorBase<TipoUtilizable>, IUtilizableConObjetivos, IUtilizableSinObjetivos
    where TipoUtilizable : ModeloUtilizable, new()
    {
        #region Controladores

        private IControladorTiradaBase ControladorTiradaDeUso { get; set; }
        public IControladorModificadorDeStatBase ControladorVentajaAlUtilizarlo { get; set; }
        public ControladorEfecto<ModeloEfecto> ControladorEfectoSobreElUsuario { get; set; }
        public ControladorEfecto<ModeloEfecto> ControladorEfectoSobreElObjetivo { get; set; }

        #endregion

        #region Constructores

        public ControladorUtilizable()
        {
        }

        public ControladorUtilizable(ModeloUtilizable _modeloUtilizable)
        {
            modelo = (TipoUtilizable) _modeloUtilizable;
        }

        #endregion

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
