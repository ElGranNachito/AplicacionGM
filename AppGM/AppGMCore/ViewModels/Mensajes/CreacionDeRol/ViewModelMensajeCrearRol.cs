namespace AppGM.Core
{
    public class ViewModelMensajeCrearRol : ViewModelVentanaConPasos<ViewModelMensajeCrearRol>
    {
        public ModeloRol rol { get; set; } = new ModeloRol();

        public ViewModelMensajeCrearRol()
        {
            mViewModelsPasos.AddRange(new ViewModelPaso<ViewModelMensajeCrearRol>[]
            {
                new ViewModelMensajeCrearRol_DatosRol(rol),
                new ViewModelMensajeCrearRol_DatosMapa(this) 
            });

            PasoActual.PropertyChanged += mHandlerPasoActualPropertyChanged;
        }
    }
}
