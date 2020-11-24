using System.IO;
using AppGM.Core;

namespace AppGM
{
    class Archivo_Windows : IArchivo
    {
        private FileInfo mArchivo;

        public string Nombre
        {
            get => mArchivo.Name; 
            set{}
        }

        public string NombreSinExtension
        {
            get => Nombre.Remove(Nombre.Length - Extension.Length);
            set{}
        }

        public string Extension
        {
            get => mArchivo.Extension; 
            set{}
        }

        public string Ruta
        {
            get => mArchivo.FullName;
            set{}
        }

        public IDirectorio DirectorioPadre
        {
            get => new Directorio_Windows(new DirectoryInfo(Path.GetFullPath(Path.Combine(Ruta, @"../"))));
            set{}
        }

        public Archivo_Windows(FileInfo infoArchivo) => mArchivo = infoArchivo;

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
    }
}