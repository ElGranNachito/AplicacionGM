namespace AppGM.Core
{
    public abstract class ControladorBase {}
    public class Controlador<TipoModelo> : ControladorBase
        where TipoModelo: ModeloBaseSK, new()
    {
        #region Miembros

        public TipoModelo modelo; 

        #endregion

        #region Funciones

        public virtual void Eliminar() { } 

        #endregion
    }
}
