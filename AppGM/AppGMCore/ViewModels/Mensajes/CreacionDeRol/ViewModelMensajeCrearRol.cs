namespace AppGM.Core.ViewModels.Mensajes
{
    public class ViewModelMensajeCrearRol : ViewModelVentanaConPasos
    {
        public ModeloRol rol { get; set; } = new ModeloRol();

        public ViewModelMensajeCrearRol()
        {
            mViewModelsPasos.AddRange(new []
                {new ViewModelMensajeCrearRol_DatosRol(rol)});
        }
    }
}
