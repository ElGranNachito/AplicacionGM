using System.IO;
using System.Text;
using System.Windows;
using AppGM.Core;
using Microsoft.Win32;

namespace AppGM
{
    class ControladorDeArchivos_Windows : IControladorDeArchivos
    {
        public string DirectorioDeTrabajo { get; set; }
        public string DirectorioEjecutable { get; set; }
        public string DirectorioImagenes { get; set; }
        public string DirectorioImagenesMapas
        {
            get => Path.Combine(DirectorioImagenes, @"Mapas\");
            set { }
        }

        public ControladorDeArchivos_Windows()
        {
            DirectorioDeTrabajo = Directory.GetCurrentDirectory();

            DirectorioImagenes = Path.Combine(DirectorioDeTrabajo, @"Media\");

            while (!Directory.Exists(DirectorioImagenes))
                DirectorioImagenes = Path.GetFullPath(Path.Combine(DirectorioImagenes, @"../"));

            DirectorioImagenes = Path.Combine(DirectorioImagenes, @"Imagenes\");
        }

        public string ObtenerPathArchivo(string path, string[] carpetasPosteriores, string nombreArchivo)
        {
            StringBuilder sb = new StringBuilder(path);

            if (Directory.Exists(path))
            {
                for (int i = 0; i < carpetasPosteriores.Length; ++i)
                    sb.Append($@"{carpetasPosteriores[i]}\");

                if (Directory.Exists(sb.ToString()))
                    sb.Append(nombreArchivo);
            }

            return sb.ToString();
        }

        public IDirectorio EncontrarDirectorio(string path, string nombre)
        {
            string pathDirectorio = Path.Combine(path, nombre);

            if(Directory.Exists(pathDirectorio))
                return new Directorio_Windows(new DirectoryInfo(pathDirectorio));

            return null;
        }

        public IArchivo MostrarDialogoAbrirArchivo(string titulo, string extensionesBuscadas, IVentana ventanaPadre)
        {
            OpenFileDialog dialogoAbrirArchivo = new OpenFileDialog();

            dialogoAbrirArchivo.DefaultExt       = extensionesBuscadas;
            dialogoAbrirArchivo.InitialDirectory = DirectorioDeTrabajo;
            dialogoAbrirArchivo.Title            = titulo;
            dialogoAbrirArchivo.Filter           = extensionesBuscadas;

            dialogoAbrirArchivo.ShowDialog((Window)ventanaPadre.ObtenerInstanciaVentana());

            return new Archivo_Windows(new FileInfo(dialogoAbrirArchivo.FileName));
        }
    }
}