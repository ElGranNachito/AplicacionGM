using System;
using System.Windows.Input;

namespace AppGM.Core
{
    public class ViewModelMensajeCrearRol_DatosMapa : ViewModelPaso<ViewModelMensajeCrearRol>
    {
        private IArchivo mArchivoMapa;

        public ICommand ComandoSeleccionarImagenMapa { get; set; }

        public string NombreMapa { get; set; }

        public string PathImagenMapa { get; set; } = string.Empty;

        public bool BorrarImagenDeLaUbicacionAnterior { get; set; }

        public ViewModelMensajeCrearRol_DatosMapa(ViewModelMensajeCrearRol viewModelCrearRol)
        {
            ComandoSeleccionarImagenMapa = new Comando(() =>
            {
                mArchivoMapa = SistemaPrincipal.ControladorDeArchivos.MostrarDialogoAbrirArchivo(
                    "Seleccionar Imagen Mapa",
                    "Formatos imagen (*.jpg *.png)|*.jpg;*.png", 
                    SistemaPrincipal.Aplicacion.VentanaPopups);

                PathImagenMapa = mArchivoMapa.Ruta;
            });
        }

        public override void Desactivar(ViewModelMensajeCrearRol vm)
        {
            if (string.IsNullOrEmpty(NombreMapa) || string.IsNullOrEmpty(PathImagenMapa))
                return;

            if(mArchivoMapa.NombreSinExtension == NombreMapa)
                return;

            IArchivo archivoViejo = mArchivoMapa.CopiarADirectorio(SistemaPrincipal.ControladorDeArchivos.DirectorioImagenesMapas, true);
            mArchivoMapa.CambiarNombre(NombreMapa);

            PathImagenMapa = mArchivoMapa.Ruta;

            //Borramos el archivo al salir de la aplicacion porque de intentar hacerlo aqui no podremos
            if (BorrarImagenDeLaUbicacionAnterior)
                SistemaPrincipal.Aplicacion.VentanaPrincipal.OnVentanaCerrada += ventana =>
                { 
                    archivoViejo.Borrar();
                };
        }

        public override bool PuedeAvanzar() => !(String.IsNullOrEmpty(NombreMapa) || String.IsNullOrEmpty(PathImagenMapa));
    }
}
