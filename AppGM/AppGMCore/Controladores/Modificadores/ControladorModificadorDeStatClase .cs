using System.Reflection;

namespace AppGM.Core
{
    class ControladorModificadorDeStatClase : ControladorModificadorDeStatBase
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