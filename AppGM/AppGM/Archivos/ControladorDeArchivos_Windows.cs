using System.IO;
using System.Windows;
using AppGM.Core;
using Microsoft.Win32;

namespace AppGM
{
    /// <summary>
    /// Implementacion de la interfaz <see cref="IControladorDeArchivos"/>
    /// </summary>
    class ControladorDeArchivos_Windows : IControladorDeArchivos
    {
        #region Propiedades

        public char CaracterSeparadorDeCarpetas => Path.DirectorySeparatorChar;

        public string DirectorioDeTrabajo { get; set; }
        public string DirectorioEjecutable { get; set; }
        public string DirectorioImagenes { get; set; }
        public string DirectorioImagenesMapas { get; set; }
        public string DirectorioControles { get; set; }

        public string DirectorioFunciones { get; set; }

        public string DirectorioAnimaciones { get; set; }

        #endregion

        #region Constructor
        public ControladorDeArchivos_Windows()
        {
            DirectorioDeTrabajo = Directory.GetCurrentDirectory();

            DirectorioImagenes = Path.Combine(DirectorioDeTrabajo, @"Media\");

            DirectorioControles = Path.Combine(DirectorioDeTrabajo, @"Paginas\");

            DirectorioImagenesMapas = Path.Combine(DirectorioImagenes, @"Imagenes\Mapas\");

            DirectorioAnimaciones = Path.Combine(DirectorioImagenes, @"Imagenes\Animaciones\");

            DirectorioFunciones = Path.Combine(DirectorioDeTrabajo, @"Funciones\");

            while (!Directory.Exists(DirectorioImagenes))
                DirectorioImagenes = Path.GetFullPath(Path.Combine(DirectorioImagenes, @"../"));

            DirectorioImagenes = Path.Combine(DirectorioImagenes, @"Imagenes\");
        }
        #endregion

        #region Funciones

        public IDirectorio EncontrarDirectorio(string path)
        {
            string pathDirectorio = Path.Combine(path);

            if (Directory.Exists(pathDirectorio))
                return new Directorio_Windows(new DirectoryInfo(pathDirectorio));

            return null;
        }

        public IArchivo MostrarDialogoAbrirArchivo(string titulo, string extensionesBuscadas, IVentana ventanaPadre)
        {
            OpenFileDialog dialogoAbrirArchivo = new OpenFileDialog();

            dialogoAbrirArchivo.DefaultExt = extensionesBuscadas;
            dialogoAbrirArchivo.InitialDirectory = DirectorioDeTrabajo;
            dialogoAbrirArchivo.Title = titulo;
            dialogoAbrirArchivo.Filter = extensionesBuscadas;

            dialogoAbrirArchivo.ShowDialog((Window)ventanaPadre.ObtenerInstanciaVentana());

            //Si el archivo seleccionado es valido creamos una nueva interfaz y la devolvemos
            if (File.Exists(dialogoAbrirArchivo.FileName))
                return new Archivo_Windows(new FileInfo(dialogoAbrirArchivo.FileName));
            
            //Si el archivo no es valido simplemente retornamos null
            return null;
        } 

        #endregion
    }
}