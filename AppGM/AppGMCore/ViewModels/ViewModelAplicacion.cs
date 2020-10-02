namespace AppGM.Core
{
    public class ViewModelAplicacion : BaseViewModel
    {
        #region Propiedades
        public string TituloVentana { get; set; } = "Aplicacion GM";
        public bool VentanaMaximizada { get; set; } = false;
        public EPaginaActual EPaginaActual { get; set; } = EPaginaActual.PaginaPrincipal; 
        #endregion

    }
}
