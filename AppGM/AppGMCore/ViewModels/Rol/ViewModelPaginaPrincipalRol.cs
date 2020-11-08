using System.Windows.Input;

namespace AppGM.Core
{
    public class ViewModelPaginaPrincipalRol : BaseViewModel, IBotonSeleccionado<object>
    {
        #region Propiedades
        //DatosRolActual DatorRolActual
        public ControladorRol ControladorRol { get; set; }

        public ICommand ComandoBotonFichas { get; set; }
        public ICommand ComandoBotonMapas { get; set; }
        public ICommand ComandoBotonRegistro { get; set; }
        public ICommand ComandoBotonTirada { get; set; }
        public ICommand ComandoBotonCombates { get; set; }
        public ICommand ComandoBotonSalir { get; set; }

        public EMenuActualRol EMenuActual { get; set; } = EMenuActualRol.NINGUNO;
        public object BotonSeleccionado { get; set; }

        #endregion

        #region Constructores
        public ViewModelPaginaPrincipalRol(ModeloRol modelo)
        {
            ControladorRol = new ControladorRol(modelo);

            ComandoBotonFichas   = new Comando(() => SistemaPrincipal.RolSeleccionado.EMenuActual = EMenuActualRol.SeleccionTipoFichas);
            ComandoBotonMapas    = new Comando(() => SistemaPrincipal.RolSeleccionado.EMenuActual = EMenuActualRol.Mapas);
            ComandoBotonCombates = new Comando(() => SistemaPrincipal.RolSeleccionado.EMenuActual = EMenuActualRol.AdministrarCombates);

            ComandoBotonSalir = new Comando(()=>
            {
                SistemaPrincipal.CerrarConexion();

                SistemaPrincipal.Aplicacion.EPaginaActual =
                        EPaginaActual.PaginaPrincipal;
            });
        } 
        #endregion
    }
}
