namespace AppGM.Core
{
    public interface IArchivo
    {
        string Nombre { get; set; }
        string NombreSinExtension { get; set; }
        string Extension { get; set; }
        string Ruta { get; set; }

        /// <summary>
        /// Devuelve la carpeta contenedora
        /// </summary>
        IDirectorio DirectorioPadre { get; set; }

        /// <summary>
        /// Copia un archivo a otro directorio
        /// </summary>
        /// <param name="directorioDestino">Ruta completa al destino</param>
        /// <param name="actualizarANuevoArchivo">Debe este archivo ahora representar a la copia</param>
        /// <returns>si <see cref="actualizarANuevoArchivo"/> es falso devuelve la copia del archivo, si es true devuelve el archivo original</returns>
        IArchivo CopiarADirectorio(string directorioDestino, bool actualizarANuevoArchivo);

        /// <summary>
        /// Mueve un archivo a otro directorio
        /// </summary>
        /// <param name="directorioDestino">Ruta completa al directorio de destino</param>
        void MoverADirectorio(string directorioDestino);

        /// <summary>
        /// Cambia el nombre de un archivo
        /// </summary>
        /// <param name="nuevoNombre">Nuevo nombre que se le dara al archivo</param>
        void CambiarNombre(string nuevoNombre);

        /// <summary>
        /// Borra el archivo
        /// </summary>
        void Borrar();
    }
}
