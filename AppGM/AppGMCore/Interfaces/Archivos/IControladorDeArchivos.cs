namespace AppGM.Core
{
    /// <summary>
    /// Inerfaz que representa una clase destinada al control de los archivos de la aplicacion
    /// </summary>
    public interface IControladorDeArchivos
    {
        #region Propiedades

        /// <summary>
        /// Caracter que separa carpetas en una ruta
        /// </summary>
        char CaracterSeparadorDeCarpetas { get; }

        /// <summary>
        /// Directorio de trabajo de la aplicacion
        /// </summary>
        string DirectorioDeTrabajo { get; set; }

        /// <summary>
        /// Directorio del ejecutable de la aplicacion
        /// </summary>
        string DirectorioEjecutable { get; set; }

        /// <summary>
        /// Directorio donde se encuentran las imagenes utilizadas por la aplicacion
        /// </summary>
        string DirectorioImagenes { get; set; }

        /// <summary>
        /// Directorio donde se encuentran las imagenes utilizadas por la aplicacion
        /// </summary>
        string DirectorioImagenesTiradas { get; set; }

        /// <summary>
        /// Directorio donde se encuentran los controles de la aplicacion 
        /// </summary>
        string DirectorioControles { get; set; }

        /// <summary>
        /// Directorio donde se encuentran las imagenes de los mapas
        /// </summary>
        string DirectorioImagenesMapas { get; set; }

        /// <summary>
        /// Directorio donde se encuentran las carpetas que contienen los fotogramas de las diversas animaciones que utiliza la aplicacion
        /// </summary>
        string DirectorioAnimaciones { get; set; }

        /// <summary>
        /// Directorio donde se encuentran los archivos .XML de las funciones credas por GuraScratch
        /// </summary>
        string DirectorioFunciones { get; set; }

        #endregion

        #region Funciones

        /// <summary>
        /// Busca un directorio
        /// </summary>
        /// <param name="path">Ruta completa del directorio</param>
        /// <returns>Una nueva instancia de <see cref="IDirectorio"/> si el directorio fue encontrado o Null si no lo fue</returns>
        IDirectorio EncontrarDirectorio(string path);

        /// <summary>
        /// Abre una ventana de dialogo para la seleccion de un archivo
        /// </summary>
        /// <param name="titulo">Titulo de la ventana</param>
        /// <param name="extensionesBuscadas">Extensiones de archivo buscadas</param>
        /// <param name="padre"><see cref="IVentana"/> 'padre' del dialogo</param>
        /// <returns>Una nueva instancia de <see cref="IArchivo"/> correspondiente al archivo seleccionado o Null si el usuario no selecciona nada</returns>
        IArchivo MostrarDialogoAbrirArchivo(string titulo, string extensionesBuscadas, IVentana padre); 

        #endregion
    }
}
