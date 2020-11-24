namespace AppGM.Core
{
    public interface IControladorDeArchivos
    {
        #region Propiedades

        string DirectorioDeTrabajo { get; set; }
        string DirectorioEjecutable { get; set; }
        string DirectorioImagenes { get; set; }
        string DirectorioImagenesMapas { get; set; }

        #endregion

        #region Funciones

        string ObtenerPathArchivo(string path, string[] carpetasPosteriores, string nombreArchivo);
        IDirectorio EncontrarDirectorio(string path, string nombre);
        IArchivo MostrarDialogoAbrirArchivo(string titulo, string extensionesBuscadas, IVentana padre); 

        #endregion
    }
}
