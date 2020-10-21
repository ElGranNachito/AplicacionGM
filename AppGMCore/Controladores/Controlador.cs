namespace AppGM.Core
{
    public class ControladorBase {}
    public class Controlador<TipoModelo> : ControladorBase
        where TipoModelo: new()
    {
        public TipoModelo modelo;
    }
}
