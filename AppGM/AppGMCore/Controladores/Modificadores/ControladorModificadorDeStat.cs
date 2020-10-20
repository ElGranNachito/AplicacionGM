using System.Reflection;

namespace AppGM.Core
{
    public interface IControladorModificadorDeStatBase
    {
        #region Controladores

        IControladorTiradaBase ControladorTiradaBase { get; set; }

        #endregion

        #region Funciones

        void AplicarModificacion(ControladorPersonaje personaje);
        void QuitarModificacion(ControladorPersonaje personaje);

        #endregion
    }

    public class ControladorModificadorDeStat : Controlador<ModeloModificadorDeStatBase>, IControladorModificadorDeStatBase
    {
        #region Implementacion Interfaz

        #region Controladores

        public IControladorTiradaBase ControladorTiradaBase { get; set; }

        #endregion

        #region Funciones

        public virtual void AplicarModificacion(ControladorPersonaje personaje)
        {
        }

        public virtual void QuitarModificacion(ControladorPersonaje personaje)
        {
        }

        #endregion 

        #endregion
    }

    public class ControladorModificadorDeStatClase : ControladorModificadorDeStat
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

        public override void AplicarModificacion(ControladorPersonaje personaje)
        {
        }

        public override void QuitarModificacion(ControladorPersonaje personaje)
        {
        }

        #endregion
    }
}