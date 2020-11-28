namespace AppGM.Core
{
    public class ViewModelMensajeCrearRol : ViewModelVentanaConPasos<ViewModelMensajeCrearRol>
    {
        public DatosCreacionRol datosRol { get; set; } = new DatosCreacionRol();

        public ViewModelMensajeCrearRol()
        {
            ModeloMapa mapaPrincipal = new ModeloMapa();

            datosRol.mapas.Add(mapaPrincipal);

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
