using System.Windows.Input;
using AppGM.Core.Delegados;

namespace AppGM.Core
{
    public class ViewModelPaginaPrincipalRol : BaseViewModel, IBotonSeleccionado<object>
    {
        #region Miembros

        private EMenuRol mEMenuActual = EMenuRol.NINGUNO;

        #endregion

        #region Propiedades
        //DatosRolActual DatorRolActual
        public ControladorRol ControladorRol { get; set; }

        public ICommand ComandoBotonFichas { get; set; }
        public ICommand ComandoBotonMapas { get; set; }
        public ICommand ComandoBotonRegistro { get; set; }
        public ICommand ComandoBotonTirada { get; set; }
        public ICommand ComandoBotonCombates { get; set; }
        public ICommand ComandoBotonSalir { get; set; }

        public EMenuRol EMenu
        {
            get => mEMenuActual;
            set
            {
                if (value == mEMenuActual)
                    return;

                EMenuRol menuAnterior = mEMenuActual;

                mEMenuActual = value;

                OnMenuCambio(menuAnterior, mEMenuActual);
            }
        }

        public object BotonSeleccionado { get; set; }

        #endregion

        #region Constructores
        public ViewModelPaginaPrincipalRol()
        {
            ControladorRol = new ControladorRol(SistemaPrincipal.ModeloRolActual);

            ComandoBotonFichas   = new Comando(() => SistemaPrincipal.RolSeleccionado.EMenu = EMenuRol.SeleccionTipoFichas);
            ComandoBotonMapas    = new Comando(() => SistemaPrincipal.RolSeleccionado.EMenu = EMenuRol.Mapas);
            ComandoBotonCombates = new Comando(() => SistemaPrincipal.RolSeleccionado.EMenu = EMenuRol.AdministrarCombates);

            ComandoBotonSalir = new Comando(()=>
            {
                BotonSeleccionado = null;

                SistemaPrincipal.GuardarDatosRol();

                SistemaPrincipal.Aplicacion.EPagina =
                        EPagina.PaginaPrincipal;
            });
        }
        #endregion

        #region Eventos

        public event DVariableCambio<EMenuRol> OnMenuCambio = delegate { };

        #endregion
    }
}