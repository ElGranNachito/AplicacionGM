using System.Collections.Generic;

namespace AppGM.Core
{
    /// <summary>
    /// Interfaz destinada a representar un directorio
    /// </summary>
    public interface IDirectorio
    {
        #region Propiedades

        /// <summary>
        /// Ruta completa del directorio
        /// </summary>
        string Ruta { get; set; }

        /// <summary>
        /// Nombre del directorio
        /// </summary>
        string Nombre { get; set; }

        /// <summary>
        /// <see cref="IDirectorio"/> que precede a este en la jerarquia y por lo tanto lo contiene
        /// </summary>
        IDirectorio DirectorioPadre { get; }

        #endregion

        #region Funciones

        /// <summary>
        /// Devuelve una lista de directorios cuyos nombres coinciden con el <paramref name="patronDeBusqueda"/>
        /// </summary>
        /// <param name="patronDeBusqueda">Patron que buscar en los directorios</param>
        /// <returns>Lista que contiene los <see cref="IDirectorio"/> que satisfacen el <paramref name="patronDeBusqueda"/></returns>
        List<IDirectorio> ObtenerDirectorios(string patronDeBusqueda);

        /// <summary>
        /// Devuelve una lista de los archivos cuyos nombres coinciden con el <paramref name="patronDeBusqueda"/>
        /// </summary>
        /// <param name="patronDeBusqueda">Patron que buscar en los directorios</param>
        /// <returns>Lista que contiene los <see cref="IArchivo"/> que satisfacen el <paramref name="patronDeBusqueda"/></returns>
        List<IArchivo> ObtenerArchivos(string patronDeBusqueda);

        /// <summary>
        /// Borra el archivo
        /// </summary>
        /// <param name="recursivo">Indica si borrar subdirectorios y subarchivos</param>
        void Borrar(bool recursivo); 

        #endregion
    }
}
