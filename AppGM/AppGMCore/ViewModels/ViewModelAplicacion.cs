using System.Threading.Tasks;
using AppGM.Core.Delegados;

namespace AppGM.Core
{
    public delegate void EventoVentana(IVentana ventana);
    public interface IVentana
    { 
        string TituloVentana { get; set; }

        object ObtenerInstanciaVentana();
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
        Task Mostrar(ViewModelMensajeBase vm, bool esperarCierre, int alto, int ancho);

        void EstablecerViewModel(ViewModelMensajeBase nuevoVM);

        bool EstaAbierta { get; }
        bool DebeEsperarCierre { get; set; }
    }
    public class ViewModelAplicacion : BaseViewModel
    {
        #region Miembros

        private EPagina mEPagina = EPagina.PaginaPrincipal;

        #endregion

        #region Propiedades
        public IVentana VentanaPrincipal { get; set; }
        public IVentanaMensaje VentanaPopups { get; set; }

        public EPagina EPagina
        {
            get => mEPagina;
            set
            {
                //Si el valor es igual a la pagina actual regresamos para no disparar el evento
                if (value == mEPagina)
                    return;

                //Hacemos una variable temporal con la pagina anterior
                EPagina paginaAnterior = mEPagina;

                mEPagina = value;

                //Disparamos el evento una vez la pagina actual ya ha sido actualizada por si el recibidor del evento quiere acceder a ella
                OnPaginaActualCambio(paginaAnterior, mEPagina);
            }
        }

        #endregion

        #region Delegatos & Eventos

        public event DVariableCambio<EPagina> OnPaginaActualCambio = delegate{};

        #endregion
    }
}
