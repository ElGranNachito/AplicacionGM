using System.Windows.Input;

namespace AppGM.Core
{
    public class ViewModelMensajeCrearRol_DatosMapa : BaseViewModel
    {
        public ICommand ComandoSeleccionarImagenMapa { get; set; }

        public string NombreMapa { get; set; }

        public string PathImagenMapa { get; set; } = string.Empty;

        public ViewModelMensajeCrearRol_DatosMapa()
        {
            ComandoSeleccionarImagenMapa = new Comando(() =>
            {
                IArchivo archivoMapa = SistemaPrincipal.ControladorDeArchivos.MostrarDialogoAbrirArchivo(
                    "Seleccionar Imagen Mapa",
                    "Formatos imagen (*.jpg *.png)|*.jpg;*.png", SistemaPrincipal.Aplicacion.VentanaPrincipal);

                PathImagenMapa = archivoMapa.Ruta;

                //TODO: Mover mapa a la carpeta de imagenes y cambiarle el nombre al del mapa
            });
        }
    }
}
