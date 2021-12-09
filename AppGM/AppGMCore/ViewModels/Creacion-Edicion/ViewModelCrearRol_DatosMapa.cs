using System;
using System.IO;
using System.Net.Mime;
using System.Windows.Input;
using CoolLogs;

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
        public byte[] ImagenMapa { get; set; }

		/// <summary>
		/// Indica si la imagen del mapa ha sido seleccionada
		/// </summary>
        public bool ImagenMapaFueSeleccionada => ImagenMapa != null;

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

				if (mArchivoMapa == null)
					return;

				try
				{
					using BinaryReader bReader =
						new BinaryReader(File.Open(mArchivoMapa.Ruta, FileMode.Open, FileAccess.Read));

					ImagenMapa = bReader.ReadBytes((int)bReader.BaseStream.Length);

					DispararPropertyChanged(nameof(ImagenMapaFueSeleccionada));
				}
				catch (Exception ex)
				{
					SistemaPrincipal.LoggerGlobal.Log(
						$"Error al intentar leer imagen {mArchivoMapa.Nombre}.{Environment.NewLine}{ex.Message}", ESeveridad.Error);
				}
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
				Imagen = ImagenMapa,
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
				ImagenMapa = vm.modeloRol.Mapas[0].Imagen;
		}

		public override void Desactivar(ViewModelCrearRol vm)
		{
			if (string.IsNullOrEmpty(NombreMapa) || ImagenMapa is null)
				return;

			if (mArchivoMapa.NombreSinExtension == NombreMapa)
				return;

            //Simbolo que indica si debemos copiar la imagen.
            //Solamente existe para no copiar imagenes durante las pruebas
			#if !NO_COPIAR_IMAGENES

            //Borramos el archivo al salir de la aplicacion porque de intentar hacerlo aqui no podremos
            if (BorrarImagenDeLaUbicacionAnterior)
                SistemaPrincipal.Aplicacion.VentanaPrincipal.OnVentanaCerrada += ventana =>
                { 
                    mArchivoMapa.Borrar();
                };

			#endif
		}

		public override bool PuedeAvanzar() => !(NombreMapa.IsNullOrWhiteSpace() || ImagenMapa is null); 

		#endregion
	}
}
