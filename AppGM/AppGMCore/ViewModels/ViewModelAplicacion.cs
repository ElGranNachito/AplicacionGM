using AppGM.Core.Delegados;

namespace AppGM.Core
{
    /// <summary>
    /// Viewmodel que representa el contenido de la ventana de la aplicacion y cualquiera de sus popups
    /// </summary>
	public class ViewModelAplicacion : ViewModel
    {
        #region Campos & Propiedades

        //--------------------------CAMPOS--------------------------------


        /// <summary>
        /// Pagina actual de la app
        /// </summary>
        private EPagina mPaginaActual;


        //------------------------PROPIEDADES-------------------------


        /// <summary>
        /// Ventana principal
        /// </summary>
        public IVentana VentanaPrincipal { get; set; }

        /// <summary>
        /// Ventana actualmente activa siendo utilizada por el usuario
        /// </summary>
        public IVentana VentanaActual { get; set; }

        /// <summary>
        /// Ventana de popups
        /// </summary>
        public IVentanaMensaje VentanaMensajePrincipal => VentanaPrincipal.VentanaMensaje.Value;

        /// <summary>
        /// Pagina actual de la aplicacion
        /// </summary>
        public EPagina PaginaActual
        {
            get => mPaginaActual;
            set
            {
                //Si el valor es igual a la pagina actual regresamos para no disparar el evento
                if (value == mPaginaActual)
                    return;

                //Hacemos una variable temporal con la pagina anterior
                PaginaAnterior = mPaginaActual;

                mPaginaActual = value;

                switch (PaginaActual)
                {
                    case EPagina.PaginaPrincipal:
	                    VentanaPrincipal.DataContextContenido = SistemaPrincipal.ObtenerInstancia<ViewModelPaginaInicio>();
	                    break;
                    case EPagina.PaginaPrincipalRol:
	                    VentanaPrincipal.DataContextContenido = SistemaPrincipal.RolSeleccionado;
	                    break;
                    case EPagina.CreacionDeRol:
	                    VentanaPrincipal.DataContextContenido = new ViewModelCrearRol();
	                    break;
                }

                //Disparamos el evento una vez la pagina actual ya ha sido actualizada por si el recibidor del evento quiere acceder a ella
                OnPaginaActualCambio(PaginaAnterior, mPaginaActual);
            }
        }

        /// <summary>
        /// Pagina que estuvo antes a <see cref="PaginaActual"/>
        /// </summary>
        public EPagina PaginaAnterior { get; private set; }

        #endregion

        #region Delegatos & Eventos

        /// <summary>
        /// Evento que se dispara cuando la pagina actaul de la aplicacion cambia
        /// </summary>
        public event DVariableCambio<EPagina> OnPaginaActualCambio = delegate{};

        #endregion
    }
}