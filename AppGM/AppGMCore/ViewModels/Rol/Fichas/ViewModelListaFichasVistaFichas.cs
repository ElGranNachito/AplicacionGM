using AppGM.Core;

namespace AppGM
{
    public class ViewModelListaFichasVistaFichas : BaseViewModel, IBotonSeleccionado<BaseViewModel>
    {
        public ViewModelListaFichas ViewModelListaFichas { get; set; } = new ViewModelListaFichas();
        public ViewModelFichaItem FichaSeleccionada { get; set; }

        public BaseViewModel BotonSeleccionado
        {
            get => FichaSeleccionada;
            set => FichaSeleccionada = (ViewModelFichaItem)value;
        }
    }
}