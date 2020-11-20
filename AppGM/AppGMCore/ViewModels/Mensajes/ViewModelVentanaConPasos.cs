using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace AppGM.Core.ViewModels.Mensajes
{
    /// <summary>
    /// View model para una ventana en la que se haya que seguir varios pasos
    /// </summary>
    public class ViewModelVentanaConPasos : ViewModelMensajeBase
    {
        protected int mIndicePasoActual = 0;

        protected List<BaseViewModel> mViewModelsPasos = new List<BaseViewModel>();

        public BaseViewModel PasoActual => mViewModelsPasos[mIndicePasoActual];

        public ICommand ComandoPasoSiguiente { get; private set; }
        public ICommand ComandoPasoAnterior { get; private set; }

        public bool PuedeAvanzar => mIndicePasoActual < mViewModelsPasos.Count;
        public bool PuedeRetroceder => mIndicePasoActual > 0;

        public ViewModelVentanaConPasos()
        {
            EstablecerComandos();
        }
        public ViewModelVentanaConPasos(List<BaseViewModel> _viewModelsPasos)
        {
            mViewModelsPasos = _viewModelsPasos;

            EstablecerComandos();
        }

        public void EstablecerIndiceActual(int nuevoIndice)
        {
            if (nuevoIndice < 0 || nuevoIndice >= mViewModelsPasos.Count)
                return;

            mIndicePasoActual = nuevoIndice;

            DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PasoActual)));
            DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuedeAvanzar)));
            DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuedeRetroceder)));
        }

        private void EstablecerComandos()
        {
            ComandoPasoSiguiente = new Comando(()=>EstablecerIndiceActual(++mIndicePasoActual));
            ComandoPasoAnterior = new Comando(()=>EstablecerIndiceActual(--mIndicePasoActual));
        }
    }
}