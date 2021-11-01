namespace AppGM.Core
{
    /// <summary>
    /// Popup de creacion de rol
    /// </summary>
    public class ViewModelCrearRol : ViewModelVentanaConPasos<ViewModelCrearRol>
    {
	    /// <summary>
        /// Datos del rol que estamos creando
        /// </summary>
        public DatosCreacionRol datosRol { get; set; } = new DatosCreacionRol();

        /// <summary>
        /// Constructor
        /// </summary>
        public ViewModelCrearRol()
        {
	        SistemaPrincipal.Atar(datosRol.modeloRol);
            SistemaPrincipal.Atar(new ViewModelRol());

	        MostrarBotonSalir = true;

            ModeloMapa mapaPrincipal = new ModeloMapa();

            datosRol.mapas.Add(mapaPrincipal);

            //Añadimos los pasos
            mViewModelsPasos.AddRange(new ViewModelPaso<ViewModelCrearRol>[]
            {
                new ViewModelCrearRol_DatosRol(datosRol.modeloRol),
                new ViewModelCrearRol_DatosMapa(mapaPrincipal),
                new ViewModelCrearRol_DatosPersonajes(datosRol, this) 
            });

            PasoActual.PropertyChanged += mHandlerPasoActualPropertyChanged;

            ComandoSalir = new Comando(() => { SistemaPrincipal.Aplicacion.PaginaActual = EPagina.PaginaPrincipal; });

            Inicializar();
        }
    }
}
