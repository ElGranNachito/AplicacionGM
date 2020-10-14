using System.Reflection;

namespace AppGM.Core
{
    public interface IControladorModificadorDeStatBase
    {
        #region Controladores

        IControladorTiradaBase ControaldorTiradaBase { get; set; }

        #endregion

        #region Funciones

        void AplicarModificacion(ControladorPersonaje<ModeloPersonaje> personaje);
        void QuitarModificacion(ControladorPersonaje<ModeloPersonaje> personaje);

        #endregion
    }

    public class ControladorModificadorDeStatBase<TipoModificador> : ControladorBase<ModeloModificadorDeStatBase>, IControladorModificadorDeStatBase
        where TipoModificador : ModeloModificadorDeStatBase, new()
    {
        #region Implementacion Interfaz

        #region Controladores

        public IControladorTiradaBase ControaldorTiradaBase { get; set; }

        #endregion

        #region Funciones

        public virtual void AplicarModificacion(ControladorPersonaje<ModeloPersonaje> personaje)
        {
        }

        public virtual void QuitarModificacion(ControladorPersonaje<ModeloPersonaje> personaje)
        {
        }

        #endregion 

        #endregion
    }

    public class ControladorModificadorDeStatClase : ControladorModificadorDeStatBase<ModeloModificadorDeStatClase>
    {
        #region Propiedades

        public object Modificacion { get; set; }

        public MemberInfo Miembro { get; set; }

        #endregion

        #region Constructor

        public ControladorModificadorDeStatClase(ModeloModificadorDeStatClase _modeloModificadorDeStatClase)
        {
            modelo = _modeloModificadorDeStatClase;
        }

        #endregion

        #region Funciones

        public override void AplicarModificacion(ControladorPersonaje<ModeloPersonaje> personaje)
        {
        }

        public override void QuitarModificacion(ControladorPersonaje<ModeloPersonaje> personaje)
        {
        }

        #endregion
    }
}