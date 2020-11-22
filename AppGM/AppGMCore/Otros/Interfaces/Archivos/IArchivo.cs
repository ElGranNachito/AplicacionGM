namespace AppGM.Core
{
    public interface IArchivo
    {
        string Nombre { get; set; }
        string Extension { get; set; }
        string Ruta { get; set; }

        IDirectorio DirectorioPadre { get; set; }

        void Borrar();
    }
}
