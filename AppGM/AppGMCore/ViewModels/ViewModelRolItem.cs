using System.Windows.Input;
using Ninject;

namespace AppGM.Core
{
    public class ViewModelRolItem : BaseViewModel
    {
        public ModeloRol ModeloRol { get; set; }
        public ICommand ComandoClickeado { get; set; }
        public ICommand ComandoMouseEnter { get; set; }
        public ICommand ComandoMouseLeave { get; set; }

        public ViewModelRolItem()
        {
            ComandoMouseEnter = new Comando(
                ()=>
                {
                    ViewModelGlobo ViewModelGlobo = SistemaPrincipal.Kernel.Get<ViewModelPaginaPrincipal>().GloboInfoRol;

                    ViewModelGlobo.GloboVisible = true;
                    ((ViewModelContenidoGloboInfoRol) ViewModelGlobo.ViewModelContenido).ModeloRol = ModeloRol;
                });
            ComandoMouseLeave = new Comando(
                () =>
                {
                    ViewModelGlobo ViewModelGlobo =
                        SistemaPrincipal.Kernel.Get<ViewModelPaginaPrincipal>().GloboInfoRol;

                    ViewModelGlobo.GloboVisible = false;
                    ((ViewModelContenidoGloboInfoRol) ViewModelGlobo.ViewModelContenido).ModeloRol = ModeloRol;
                }
            );
        }
    }

}
