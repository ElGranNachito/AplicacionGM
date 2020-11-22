using System.IO;
using AppGM.Core;

namespace AppGM
{
    class Archivo_Windows : IArchivo
    {
        private FileInfo mArchivo;

        public string Nombre { get; set; }

        public string Extension { get; set; }

        public string Ruta { get; set; }

        public Archivo_Windows(FileInfo infoArchivo)
        {
            Nombre          = infoArchivo.Name;
            Extension       = infoArchivo.Extension;
            Ruta            = infoArchivo.FullName;

            mArchivo = infoArchivo;
        }

        public IDirectorio DirectorioPadre
        {
            get => new Directorio_Windows(new DirectoryInfo(Path.GetFullPath(Path.Combine(Ruta, @"../"))));
            set{}
        }

        public void Borrar() => mArchivo.Delete();
    }
}
