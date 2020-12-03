using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace AppGM.Core
{
    public delegate void dPasoCambio(BaseViewModel vmAnterior, BaseViewModel vmNuevo);

    /// <summary>
    /// View model para una ventana en la que se haya que seguir varios pasos
    /// </summary>
    public class ViewModelVentanaConPasos<TipoViewModel> : ViewModelMensajeBase
        where TipoViewModel: ViewModelVentanaConPasos<TipoViewModel>
    {
        #region Miembros

        protected PropertyChangedEventHandler mHandlerPasoActualPropertyChanged;

        protected int mIndicePasoActual = 0;

        protected List<ViewModelPaso<TipoViewModel>> mViewModelsPasos = new List<ViewModelPaso<TipoViewModel>>();

        public event dPasoCambio OnAvanzarPaso = delegate { };
        public event dPasoCambio OnRetrocederPaso = delegate { };

        #endregion

        #region Propiedades

        public ViewModelPaso<TipoViewModel> PasoActual => mViewModelsPasos[mIndicePasoActual];
        public bool PuedeAvanzar    => mIndicePasoActual < mViewModelsPasos.Count - 1 && PasoActual.PuedeAvanzar();
        public bool PuedeRetroceder => mIndicePasoActual > 0;

        public ICommand ComandoPasoSiguiente { get; private set; }
        public ICommand ComandoPasoAnterior { get; private set; }

        #endregion

        #region Constructores

        public ViewModelVentanaConPasos()
        {
            mHandlerPasoActualPropertyChanged = (sender, args) =>
            {
                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuedeAvanzar)));
            };

            EstablecerComandos();
        }
        public ViewModelVentanaConPasos(List<ViewModelPaso<TipoViewModel>> _viewModelsPasos)
        {
            mViewModelsPasos = _viewModelsPasos;

            mHandlerPasoActualPropertyChanged = (sender, args) =>
            {
                if(args.PropertyName != nameof(PuedeAvanzar))
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuedeAvanzar)));
            };

            PasoActual.PropertyChanged += mHandlerPasoActualPropertyChanged;

            EstablecerComandos();
        }
        #endregion

        #region Funciones
        public void EstablecerIndiceActual(int nuevoIndice)
        {
            if (nuevoIndice < 0 || nuevoIndice >= mViewModelsPasos.Count)
                return;

            if (mIndicePasoActual < nuevoIndice)
                OnAvanzarPaso(PasoActual, mViewModelsPasos[nuevoIndice]);
            else
                OnRetrocederPaso(PasoActual, mViewModelsPasos[nuevoIndice]);

            PasoActual.Desactivar((TipoViewModel)this);
            PasoActual.PropertyChanged -= mHandlerPasoActualPropertyChanged;

            mIndicePasoActual = nuevoIndice;

            PasoActual.Activar((TipoViewModel)this);
            PasoActual.PropertyChanged += mHandlerPasoActualPropertyChanged;

            DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PasoActual)));
            DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuedeAvanzar)));
            DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuedeRetroceder)));
        }

        private void EstablecerComandos()
        {
            ComandoPasoSiguiente = new Comando(() => EstablecerIndiceActual(mIndicePasoActual + 1));
            ComandoPasoAnterior = new Comando(() => EstablecerIndiceActual(mIndicePasoActual - 1));
        } 
        #endregion
    }
}