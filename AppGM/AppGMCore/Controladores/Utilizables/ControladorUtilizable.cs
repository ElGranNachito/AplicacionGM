namespace AppGM.Core
{
    //TODO: Decidir si es abstracta o no
    public class ControladorUtilizable : Controlador<ModeloUtilizable>, IUtilizable
    {
        #region Controladores

        private IControladorTiradaBase ControladorTiradaDeUso { get; set; }
        public IControladorModificadorDeStatBase ControladorVentajaAlUtilizarlo { get; set; }
        public ControladorEfecto ControladorEfectoSobreElUsuario { get; set; }
        public ControladorEfecto ControladorEfectoSobreElObjetivo { get; set; }

        #endregion

        #region Constructores

        public ControladorUtilizable(ModeloUtilizable _modeloUtilizable)
	        : base(_modeloUtilizable)
        {
            CargarVariablesYTiradas();
        }

        #endregion

        #region Eventos

        public delegate void dUtilizarHabilidad(ControladorHabilidad habilidad, ControladorPersonaje usuario, ControladorPersonaje[] objetivos);

        public event dUtilizarHabilidad OnUtilizarHabilidad = delegate { };

        #endregion

        #region Funciones

        public virtual void Utilizar(ControladorPersonaje usuario, ControladorPersonaje[] objetivos, object parametroExtra, object segundoParametroExtra)
        {
            //TODO: Realizar la tirada de utilizacion. Verificar si le da al objetivo
        }

        public virtual void Utilizar(ControladorPersonaje usuario, object parametroExtra, object segundoParametroExtra)
        {
            //TODO: Realizar la tirada de utilizacion.
        }

        public virtual bool PuedeUtilizar(ControladorPersonaje usuario, ControladorPersonaje[] objetivos)
        {
	        return false;
        }

        #endregion
    }
}
