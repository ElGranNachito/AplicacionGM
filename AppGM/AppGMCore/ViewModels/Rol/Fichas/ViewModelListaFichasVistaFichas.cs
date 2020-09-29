using AppGM.Core;

namespace AppGM
{
    public class ViewModelListaFichasVistaFichas : BaseViewModel
    {
        public ViewModelListaFichas ViewModelListaFichas { get; set; } = new ViewModelListaFichas();
        public ViewModelFichaItem FichaSeleccionada { get; set; }
    }
}
