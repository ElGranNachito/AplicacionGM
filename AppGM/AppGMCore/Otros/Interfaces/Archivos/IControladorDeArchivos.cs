using System;

namespace AppGM.Core
{
    public interface IControladorDeArchivos
    {
        #region Propiedades

        char CaracterSeparadorDeCarpetas { get; }

        string DirectorioDeTrabajo { get; set; }
        string DirectorioEjecutable { get; set; }
        string DirectorioImagenes { get; set; }
        string DirectorioImagenesMapas { get; set; }

        string DirectorioAnimaciones { get; set; }

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
