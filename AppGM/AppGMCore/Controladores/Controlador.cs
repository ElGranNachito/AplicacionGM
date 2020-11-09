namespace AppGM.Core
{
    public class ControladorBase {}
    public class Controlador<TipoModelo> : ControladorBase
        where TipoModelo: new()
    {
        #region Miembros

        public TipoModelo modelo; 

        #endregion

        #region Funciones

        public virtual void Eliminar() { } 

        #endregion
    }
}
