using System;
using System.Windows.Input;

namespace AppGM.Core
{
    /// <summary>
    /// VM para la creacion de un mapa de un nuevo rol
    /// </summary>
    public class ViewModelMensajeCrearRol_DatosMapa : ViewModelPaso<ViewModelMensajeCrearRol>
    {
	    #region Campos & Propiedades

        //----------------------------------CAMPOS---------------------------------------

        /// <summary>
        /// Modelo del mapa que crearemos
        /// </summary>
		private ModeloMapa mMapa;

        /// <summary>
        /// Archivo de la imagen del mapa
        /// </summary>
		private IArchivo mArchivoMapa;


        //-------------------------------PROPIEDADES--------------------------------------

        /// <summary>
        /// Comando que se ejecutara cuando el usuario presione el boton 'Seleccionar Imagen'
        /// </summary>
		public ICommand ComandoSeleccionarImagenMapa { get; set; }

        /// <summary>
        /// Nombre del mapa
        /// </summary>
        public string NombreMapa     { get; set; } = string.Empty;

        /// <summary>
        /// Ruta a la imagen del mapa
        /// </summary>
        public string PathImagenMapa { get; set; } = string.Empty;

        /// <summary>
        /// Indica si borrar la imagen de su ubicacion anterior
        /// </summary>
        public bool BorrarImagenDeLaUbicacionAnterior { get; set; }

		#endregion

		#region Constructor

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

		#endregion

		#region Funciones

		public override void Desactivar(ViewModelMensajeCrearRol vm)
		{
			if (string.IsNullOrEmpty(NombreMapa) || string.IsNullOrEmpty(PathImagenMapa))
				return;

			if (mArchivoMapa.NombreSinExtension == NombreMapa)
				return;

            //Simbolo que indica si debemos copiar la imagen.
            //Solamente existe para no copiar imagenes durante las pruebas
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

		#endregion
	}
}
