using AppGM.Core;

namespace AppGM
{
    public class ViewModelListaFichasVistaFichas : BaseViewModel, IBotonSeleccionado<BaseViewModel>
    {
        #region Propiedades
        public ViewModelListaFichas ViewModelListaFichas { get; set; } = new ViewModelListaFichas();
        public ViewModelFichaItem FichaSeleccionada { get; set; }

        public BaseViewModel BotonSeleccionado
        {
            get => FichaSeleccionada;
            set => FichaSeleccionada = (ViewModelFichaItem)value;
        } 
        #endregion
    }
}