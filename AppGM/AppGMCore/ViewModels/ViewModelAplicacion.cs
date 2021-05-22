using AppGM.Core.Delegados;

namespace AppGM.Core
{
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
