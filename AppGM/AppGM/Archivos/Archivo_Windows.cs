using System.IO;
using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// Implementacion de la interfaz <see cref="IArchivo"/> para WPF, es decir windows
    /// </summary>
    class Archivo_Windows : IArchivo
    {
        #region Campos & Propiedades

        //-----------------------------------CAMPOS---------------------------------


        /// <summary>
        /// Archivo
        /// </summary>
        private FileInfo mArchivo; 


        //--------------------------------PROPIEDADES--------------------------------

        /// <summary>
        /// Nombre del archivo, incluyendo su extension
        /// </summary>
        public string Nombre
        {
            get => mArchivo.Name;
            set { }
        }

        /// <summary>
        /// Nombre del archivo, sin incluir su extension
        /// (Por si con el nombre era dificil de adivinar)
        /// </summary>
        public string NombreSinExtension
        {
            get => Nombre.Remove(Nombre.Length - Extension.Length);
            set { }
        }

        /// <summary>
        /// Extension del archivo
        /// </summary>
        public string Extension
        {
            get => mArchivo.Extension;
            set { }
        }

        /// <summary>
        /// Ruta completa del archivo
        /// </summary>
        public string Ruta
        {
            get => mArchivo.FullName;
            set { }
        }

        /// <summary>
        /// Directorio que contiene al archivo
        /// </summary>
        public IDirectorio DirectorioPadre => new Directorio_Windows(new DirectoryInfo(Path.GetFullPath(Path.Combine(Ruta, @"../"))));

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="infoArchivo">Archivo que representara</param>
        public Archivo_Windows(FileInfo infoArchivo) => mArchivo = infoArchivo; 

        #endregion

        #region Funciones
        public void CambiarNombre(string nuevoNombre)
        {
            string nuevaRuta = Path.Combine(Ruta.Remove(Ruta.Length - Nombre.Length, Nombre.Length), nuevoNombre + Extension);

            File.Move(Ruta, nuevaRuta);

            mArchivo = new FileInfo(nuevaRuta);
        }

        public IArchivo CopiarADirectorio(string directorioDestino, bool actualizarANuevoArchivo)
        {
            string nuevaRuta = Path.Combine(directorioDestino, Nombre);

            mArchivo.CopyTo(nuevaRuta);

            if (actualizarANuevoArchivo)
            {
                string rutaVieja = Ruta;

                mArchivo = new FileInfo(nuevaRuta);

                return new Archivo_Windows(new FileInfo(rutaVieja));
            }

            return new Archivo_Windows(new FileInfo(nuevaRuta));
        }

        public void MoverADirectorio(string directorioDestino)
        {
            string nuevaRuta = Path.Combine(directorioDestino, Nombre);

            File.Move(Ruta, nuevaRuta);

            mArchivo = new FileInfo(nuevaRuta);
        }

        public void Borrar() => mArchivo.Delete(); 
        #endregion
    }
}