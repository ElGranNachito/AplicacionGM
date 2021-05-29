namespace AppGM.Core
{
    /// <summary>
    /// Popup de creacion de rol
    /// </summary>
    public class ViewModelMensajeCrearRol : ViewModelVentanaConPasos<ViewModelMensajeCrearRol>
    {
        /// <summary>
        /// Datos del rol que estamos creando
        /// </summary>
        public DatosCreacionRol datosRol { get; set; } = new DatosCreacionRol();

        /// <summary>
        /// Constructor
        /// </summary>
        public ViewModelMensajeCrearRol()
        {
            ModeloMapa mapaPrincipal = new ModeloMapa();

            datosRol.mapas.Add(mapaPrincipal);

            //Añadimos los pasos
            mViewModelsPasos.AddRange(new ViewModelPaso<ViewModelMensajeCrearRol>[]
            {
                new ViewModelMensajeCrearRol_DatosRol(datosRol.modeloRol),
                new ViewModelMensajeCrearRol_DatosMapa(mapaPrincipal),
                new ViewModelMensajeCrearRol_DatosPersonajes(datosRol, this) 
            });

            PasoActual.PropertyChanged += mHandlerPasoActualPropertyChanged;
        }
    }
}
