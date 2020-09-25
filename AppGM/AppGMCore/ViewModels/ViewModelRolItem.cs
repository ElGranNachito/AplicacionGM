using System.Windows.Input;

namespace AppGM.Core
{
    public class ViewModelRolItem : BaseViewModel
    {
        #region Propiedades
        public ModeloRol ModeloRol { get; set; }
        public ICommand ComandoClickeado { get; set; }
        public ICommand ComandoMouseEnter { get; set; }
        public ICommand ComandoMouseLeave { get; set; }

        #endregion

        #region Constructores
        public ViewModelRolItem()
        {
            ComandoMouseEnter = new Comando(
                () =>
                {
                    //Obtenemos el view model del globo de la pagina principal
                    ViewModelGlobo ViewModelGlobo = 
                        SistemaPrincipal.ObtenerInstancia<ViewModelPaginaPrincipal>().GloboInfoRol;

                    //Lo hacemos visible
                    ViewModelGlobo.GloboVisible = true;

                    //Establecemos la propiedad de ModeloRol a nuestro ModeloRol
                    ((ViewModelContenidoGloboInfoRol)ViewModelGlobo.ViewModelContenido).ModeloRol = ModeloRol;
                });
            ComandoMouseLeave = new Comando(
                () =>
                {
                    //Obtenemos el view model del globo de la pagina principal
                    ViewModelGlobo ViewModelGlobo =
                        SistemaPrincipal.ObtenerInstancia<ViewModelPaginaPrincipal>().GloboInfoRol;

                    //Lo hacemos invisible
                    ViewModelGlobo.GloboVisible = false;
                }
            );

            ComandoClickeado = new Comando(
                () =>
                {
                    //Cargamos el rol seleccionado
                    SistemaPrincipal.CargarRol(ModeloRol);

                    //Cambiamos la pagina actual de la aplicacion
                    SistemaPrincipal.ObtenerInstancia<ViewModelAplicacion>().EPaginaActual =
                        EPaginaActual.PaginaPrincipalRol;
                });
        } 
        #endregion
    }

}
