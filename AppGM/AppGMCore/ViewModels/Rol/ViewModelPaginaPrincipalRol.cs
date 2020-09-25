using System.Windows.Input;

namespace AppGM.Core
{
    public class ViewModelPaginaPrincipalRol : BaseViewModel
    {
        #region Propiedades
        //DatosRolActual DatorRolActual
        public ControladorRol ControladorRol { get; set; }

        public ICommand ComandoBotonFichas { get; set; }
        public ICommand ComandoBotonMapas { get; set; }
        public ICommand ComandoBotonRegistro { get; set; }
        public ICommand ComandoBotonTirada { get; set; }
        public ICommand ComandoBotonCombates { get; set; }

        public EMenuActualRol EMenuActual { get; set; } = EMenuActualRol.NINGUNO;

        #endregion

        #region Constructores
        public ViewModelPaginaPrincipalRol(ModeloRol modelo)
        {
            ControladorRol = new ControladorRol(modelo);

            ComandoBotonFichas = new Comando(()=>SistemaPrincipal.ObtenerInstancia<ViewModelPaginaPrincipalRol>().EMenuActual = EMenuActualRol.SeleccionTipoFichas);
        } 
        #endregion
    }
}
