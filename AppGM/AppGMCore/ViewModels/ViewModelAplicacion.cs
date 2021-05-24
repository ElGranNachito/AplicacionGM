using AppGM.Core.Delegados;

namespace AppGM.Core
{
    /// <summary>
    /// Viewmodel que representa el contenido de la ventana de la aplicacion y cualquiera de sus popups
    /// </summary>
	public class ViewModelAplicacion : BaseViewModel
    {
        #region Campos & Propiedades

        //--------------------------CAMPOS--------------------------------


        /// <summary>
        /// Pagina actual de la app
        /// </summary>
        private EPagina mEPagina = EPagina.PaginaPrincipal;


        //------------------------PROPIEDADES-------------------------


        /// <summary>
        /// Ventana principal
        /// </summary>
        public IVentana VentanaPrincipal { get; set; }

        /// <summary>
        /// Ventana de popups
        /// </summary>
        public IVentanaMensaje VentanaPopups { get; set; }

        /// <summary>
        /// Pagina actual de la aplicacion
        /// </summary>
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

        /// <summary>
        /// Evento que se dispara cuando la pagina actaul de la aplicacion cambia
        /// </summary>
        public event DVariableCambio<EPagina> OnPaginaActualCambio = delegate{};

        #endregion
    }
}