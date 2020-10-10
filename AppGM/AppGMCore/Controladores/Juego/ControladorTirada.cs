namespace AppGM.Core
{
    public interface IControladorTiradaBase
    {
        #region Propiedades
        
        int Resultado { get; set; }

        #endregion

        #region Funciones

        void RealizarTirada(object parametro);

        #endregion
    }

    public abstract class ControladorTiradaBase<TipoTirada> : ControladorBase<TipoTirada>, IControladorTiradaBase
        where TipoTirada : ModeloTiradaBase, new()
    {
        #region Implementacion Interfaz
        public int Resultado { get; set; }

        public virtual void RealizarTirada(object parametro) { } 
        #endregion
    }

    public class ControladorTiradaVariable : ControladorTiradaBase<ModeloTiradaVariable>
    {
        #region Constructor

        public ControladorTiradaVariable(ModeloTiradaVariable _modelo)
        {
            modelo = _modelo;
        }

        #endregion

        #region Funciones

        public override void RealizarTirada(object parametro)
        {

        }

        #endregion
    }

    public class ControladorTiradaStat : ControladorTiradaBase<ModeloTiradaStat>
    {
        #region Constructor

        public ControladorTiradaStat(ModeloTiradaStat _modeloTiradaStat)
        {
            modelo = _modeloTiradaStat;
        }

        #endregion

        #region Funciones

        public override void RealizarTirada(object p)
        {
            var personaje = (ControladorPersonaje<ModeloPersonaje>) p;

            personaje.SufrirDaño(500, ETipoDeDaño.Explosivo, null);

            //TODO: Tirar 3d6
        }

        #endregion
    }

    public class ControladorTiradaDaño : ControladorTiradaBase<ModeloTiradaDeDaño>
    {
        #region Constructor

        public ControladorTiradaDaño(ModeloTiradaDeDaño _modeloTiradaStat)
        {
            modelo = _modeloTiradaStat;
        }

        #endregion

        #region Funciones

        public override void RealizarTirada(object p)
        {
            var personaje = (ControladorPersonaje<ModeloPersonaje>)p;

            personaje.SufrirDaño(500, ETipoDeDaño.Explosivo, null);

            //TODO: Tirar 3d6
        }

        #endregion
    }
}