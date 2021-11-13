using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// VM destinado a un control que muestre ambos, la lista de fichas disposnibles (<see cref="ViewModelListaFichas"/>)
    /// y una vista de la ficha actualmente seleccionada (<see cref="FichaSeleccionada"/>)
    /// </summary>
    public class ViewModelListaFichasVistaFichas : ViewModel, IBotonSeleccionado<ViewModel>
    {
        #region Propiedades
        public ViewModelListaFichas ViewModelListaFichas { get; set; } = new ViewModelListaFichas();
        public ViewModelFichaPersonaje FichaSeleccionada { get; set; }

        public ViewModel BotonSeleccionado
        {
            get => FichaSeleccionada;
            set => FichaSeleccionada = (ViewModelFichaPersonaje)value;
        }

        #endregion
    }
}