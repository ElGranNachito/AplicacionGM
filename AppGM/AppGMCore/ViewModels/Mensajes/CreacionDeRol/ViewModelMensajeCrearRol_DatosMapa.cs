using System;
using System.Windows.Input;

namespace AppGM.Core
{
    public class ViewModelMensajeCrearRol_DatosMapa : ViewModelPaso<ViewModelMensajeCrearRol>
    {
        private ModeloMapa mMapa;
        private IArchivo mArchivoMapa;

        public ICommand ComandoSeleccionarImagenMapa { get; set; }

        public string NombreMapa     { get; set; } = string.Empty;

        public string PathImagenMapa { get; set; } = string.Empty;

        public bool BorrarImagenDeLaUbicacionAnterior { get; set; }

        public ViewModelMensajeCrearRol_DatosMapa(ModeloMapa _mapa)
        {
            mMapa = _mapa;

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

            #if !NO_COPIAR_IMAGENES

            IArchivo archivoViejo = mArchivoMapa.CopiarADirectorio(SistemaPrincipal.ControladorDeArchivos.DirectorioImagenesMapas, true);
            mArchivoMapa.CambiarNombre(NombreMapa);

            

            //Borramos el archivo al salir de la aplicacion porque de intentar hacerlo aqui no podremos
            if (BorrarImagenDeLaUbicacionAnterior)
                SistemaPrincipal.Aplicacion.VentanaPrincipal.OnVentanaCerrada += ventana =>
                { 
                    archivoViejo.Borrar();
                };

            #endif

            PathImagenMapa = mArchivoMapa.Ruta;
        }

        public override bool PuedeAvanzar() => !(String.IsNullOrEmpty(NombreMapa) || String.IsNullOrEmpty(PathImagenMapa));
    }
}
