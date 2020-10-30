namespace AppGM.Core
{
    public delegate void EventoVentana(IVentana ventana);
    public interface IVentana
    { 
        string TituloVentana { get; set; }

        void CerrarVentana();
        void Maximizar();
        void Minimizar();
        void Normalizar();
        void EstablecerTamaño(Vector2 nuevoTamaño);
        void EstablecerTamañoX(float x);
        void EstablecerTamañoY(float y);
        bool EstaMaximizada();
        Vector2 ObtenerPosicionDelMouse(); 
        Vector2 ObtenerTamaño();

        event EventoVentana OnTamañoModificado;
        event EventoVentana OnEstadoModificado;
        event EventoVentana OnTituloModificado;
        event EventoVentana OnMouseMovido;
    }
    public class ViewModelAplicacion : BaseViewModel
    {
        #region Propiedades
        public IVentana VentanaPrincipal { get; set; }
        public EPaginaActual EPaginaActual { get; set; } = EPaginaActual.PaginaPrincipal;

        #endregion
    }
}
