namespace AppGM.Core
{
    /// <summary>
    /// Popup de creacion de rol
    /// </summary>
    public class ViewModelCrearRol : ViewModelVentanaConPasos<ViewModelCrearRol>
    {
	    public ModeloRol modeloRol;

        /// <summary>
        /// Constructor
        /// </summary>
        public ViewModelCrearRol()
        {
	        SistemaPrincipal.CrearInstanciaDatosRol(0);

            modeloRol = new ModeloRol();

            SistemaPrincipal.Atar(modeloRol);

	        //SistemaPrincipal.DatosRolSeleccionado.BaseDeDatos.Database.EnsureDeleted();
			//SistemaPrincipal.DatosRolSeleccionado.BaseDeDatos.Database.EnsureCreated();

            SistemaPrincipal.GuardarModelo(SistemaPrincipal.ModeloRolActual);
            SistemaPrincipal.GuardarDatos();

            EventoVentana ventanaCerradaHandler = null;

            ventanaCerradaHandler = async ventana =>
            {
	            await SistemaPrincipal.ModeloRolActual.Eliminar();
            };

            SistemaPrincipal.Aplicacion.VentanaActual.OnVentanaCerrada += ventanaCerradaHandler;

            OnResultadoEstablecido += resultado =>
            {
	            SistemaPrincipal.Aplicacion.VentanaActual.OnVentanaCerrada -= ventanaCerradaHandler;
            };

	            //Añadimos los pasos
            mViewModelsPasos.AddRange(new ViewModelPaso<ViewModelCrearRol>[]
            {
                new ViewModelCrearRol_DatosRol(this),
                new ViewModelCrearRol_DatosMapa(this),
                new ViewModelDatosPersonajesRol(this) 
            });

            PasoActual.PropertyChanged += mHandlerPasoActualPropertyChanged;

            ComandoCancelar = new Comando(async () =>
            {
	            SistemaPrincipal.EliminarModelo(modeloRol);
	            SistemaPrincipal.Desatar<ModeloRol>();
                SistemaPrincipal.Desatar<DatosRol>();

	            await SistemaPrincipal.GuardarDatosAsync();

                Resultado = EResultadoViewModel.Cancelar;

                SistemaPrincipal.Aplicacion.PaginaActual = EPagina.PaginaPrincipal;
            });

            ComandoFinalizar = new Comando(async () =>
            {
	            var mapaRol = ((ViewModelCrearRol_DatosMapa) mViewModelsPasos[1]).CrearMapa();

                modeloRol.Mapas.Add(mapaRol);
                modeloRol.ClimaHorarioGlobal = new ModeloClimaHorario
                {
	                Clima = EClima.Soleado,
	                Viento = EViento.Brisa,
	                Humedad = EHumedad.Humedad,
	                Temperatura = ETemperatura.Frio,

	                DiaSemana = EDiaSemana.Viernes
                };

                await SistemaPrincipal.GuardarModeloAsync(mapaRol);
                await SistemaPrincipal.GuardarModeloAsync(modeloRol.ClimaHorarioGlobal);

                modeloRol.EsValido = true;

                await SistemaPrincipal.GuardarDatosAsync();

                SistemaPrincipal.DatosRolSeleccionado.CerrarConexion();

                SistemaPrincipal.Desatar<DatosRol>();
                SistemaPrincipal.Desatar<ModeloRol>();

                await SistemaPrincipal.CargarRolAsincronicamente(modeloRol.Id);

                SistemaPrincipal.Aplicacion.PaginaActual = EPagina.PaginaPrincipalRol;

	            Resultado = EResultadoViewModel.Finalizar;
            });

            ComandoGuardar = new Comando(async () =>
            {
	            await SistemaPrincipal.GuardarDatosAsync();
            });

            Inicializar();
        }
    }
}
