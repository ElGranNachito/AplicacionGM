namespace AppGMCore
{
    class ControladorBase<TipoModelo>
        where TipoModelo: new()
    {
        public TipoModelo modelo = new TipoModelo();
    }
}
