namespace AppGM.Core
{
    public class ViewModelMensajeBase : BaseViewModel
    {
        public string Titulo { get; set; } = "Sin nombre";

        protected IVentana mVentana => SistemaPrincipal.Aplicacion.VentanaPopups;
    }
}
