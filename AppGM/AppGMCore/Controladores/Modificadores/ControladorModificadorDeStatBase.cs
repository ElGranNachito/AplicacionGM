using System.Reflection;

namespace AppGM.Core
{
    public class ControladorModificadorDeStatBase<TipoModificador> : ControladorBase<ModeloModificadorDeStatBase>
    {
        #region Controladores

        public ControladorTiradaBase<ModeloTiradaBase> ControaldorTiradaBase { get; set; }

        #endregion

        #region Funciones

        public virtual void AplicarModificacion(ControladorPersonaje<ModeloPersonaje> p)
        {
        }

        public virtual void QuitarModificacion(ControladorPersonaje<ModeloPersonaje> p)
        {
        }

        #endregion
    }

    public class ControladorModificadorDeStatClase : ControladorModificadorDeStatBase<ModeloModificadorDeStatClase>
    {
        #region Propiedades

        public object Modificacion { get; set; }

        public MemberInfo Miembro { get; set; }

        #endregion

        #region Funciones

        public override void AplicarModificacion(ControladorPersonaje<ModeloPersonaje> p)
        {
        }

        public override void QuitarModificacion(ControladorPersonaje<ModeloPersonaje> p)
        {
        }

        #endregion
    }
}