using System;
using System.IO;
using System.Windows.Input;

namespace AppGM.Core
{
    /// <summary>
    /// VM para la creacion de un mapa de un nuevo rol
    /// </summary>
    public class ViewModelCrearRol_DatosMapa : ViewModelPaso<ViewModelCrearRol>
    {
	    #region Campos & Propiedades

        //----------------------------------CAMPOS---------------------------------------

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
        public string NombreMapa { get; set; } = string.Empty;

        /// <summary>
        /// Ruta a la imagen del mapa
        /// </summary>
        public string PathAbsolutoImagenMapa { get; set; } 

        /// <summary>
        /// Indica si borrar la imagen de su ubicacion anterior
        /// </summary>
        public bool BorrarImagenDeLaUbicacionAnterior { get; set; }

		#endregion

		#region Constructor

		public ViewModelCrearRol_DatosMapa(ViewModelCrearRol _contenedor)
			:base(_contenedor)
		{
			ComandoSeleccionarImagenMapa = new Comando(() =>
			{
				mArchivoMapa = SistemaPrincipal.ControladorDeArchivos.MostrarDialogoAbrirArchivo(
					"Seleccionar Imagen Mapa",
					"Formatos imagen (*.jpg *.png)|*.jpg;*.png",
					SistemaPrincipal.Aplicacion.VentanaActual);

                if(mArchivoMapa == null)
                    return;

				PathAbsolutoImagenMapa = mArchivoMapa.Ruta;

				DispararPropertyChanged(PathAbsolutoImagenMapa);
			});
		}

		#endregion

		#region Funciones

		/// <summary>
		/// Crea un nuevo <see cref="ModeloMapa"/> con los datos ingresados por el usuario
		/// </summary>
		/// <returns></returns>
		public ModeloMapa CrearMapa()
		{
			var nuevoMapa = new ModeloMapa
			{
				EFormatoImagen = Enum.Parse<EFormatoImagen>(mArchivoMapa.Extension.Remove(0, 1), true),
				NombreMapa = NombreMapa,
				Rol = SistemaPrincipal.ModeloRolActual,
			};

			//Ambiente hardcodeadito
			var nuevoAmbiente = new ModeloAmbiente
			{
				CaracteristicasAmbiente = ECaracteristicasAmbiente.Ligacion,
				HumedadActual = 0.5f,
				TemperaturaActual = -9.65f,
				CantidadCasillas = 10,
				RolAlQuePertenece = SistemaPrincipal.ModeloRolActual,
				MapaDelAmbiente = nuevoMapa,
			};

			nuevoMapa.Ambiente = nuevoAmbiente;

			return nuevoMapa;
		}

		public override void Activar(ViewModelCrearRol vm)
		{
			if (vm.modeloRol.Mapas.Count > 0)
				PathAbsolutoImagenMapa = Path.Combine(SistemaPrincipal.ControladorDeArchivos.DirectorioImagenesMapas, vm.modeloRol.Mapas[0].NombreMapa + Enum.GetName(vm.modeloRol.Mapas[0].EFormatoImagen));
		}

		public override void Desactivar(ViewModelCrearRol vm)
		{
			if (string.IsNullOrEmpty(NombreMapa) || string.IsNullOrEmpty(PathAbsolutoImagenMapa))
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
		}

		public override bool PuedeAvanzar() => !(String.IsNullOrEmpty(NombreMapa) || String.IsNullOrEmpty(PathAbsolutoImagenMapa)); 

		#endregion
	}
}
