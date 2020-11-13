using System;
using System.Threading.Tasks;

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
        event EventoVentana OnMouseDown;
        event EventoVentana OnMouseUp;
        event EventoVentana OnVentanaCerrada;
        event EventoVentana OnVentanaAbierta;
        event EventoVentana OnFotogramaActualizado;
    }

    public interface IVentanaMensaje : IVentana
    {
        /// <summary>
        /// Muestra la el mensaje en pantalla por sobre la ventana principal
        /// </summary>
        /// <param name="vm">View model del contenido el mensaje</param>
        /// <param name="esperarCierre">Si el valor es <see cref="true"/>la ventana principal queda bloqueada hasta que el mensaje se cierre</param>
        Task Mostrar(ViewModelMensajeBase vm, bool esperarCierre);

        bool EstaAbierta { get; }
        bool DebeEsperarCierre { get; set; }
    }
    public class ViewModelAplicacion : BaseViewModel
    {
        #region Propiedades
        public IVentana VentanaPrincipal { get; set; }
        public IVentanaMensaje VentanaPopups { get; set; }
        public EPaginaActual EPaginaActual { get; set; } = EPaginaActual.PaginaPrincipal;

        #endregion
    }
}
