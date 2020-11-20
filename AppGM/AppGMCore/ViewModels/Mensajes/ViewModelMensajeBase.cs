namespace AppGM.Core
{
    public class ViewModelMensajeBase : BaseViewModel
    {
        public string Titulo { get; set; } = "Sin nombre";
        public string ColorFondo { get; set; } = "000000";

        protected IVentana mVentana => SistemaPrincipal.Aplicacion.VentanaPopups;
    }
}
