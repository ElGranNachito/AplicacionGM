namespace AppGM.Core
{
    public interface IControladorDeArchivos
    {
        string DirectorioDeTrabajo { get; set; }
        string DirectorioEjecutable { get; set; }
        string DirectorioImagenes { get; set; }
        string DirectorioImagenesMapas { get; set; }

        string ObtenerPathArchivo(string path, string[] carpetasPosteriores, string nombreArchivo);
        IDirectorio EncontrarDirectorio(string path, string nombre);
        IArchivo MostrarDialogoAbrirArchivo(string titulo, string extensionesBuscadas, IVentana padre);
    }
}
