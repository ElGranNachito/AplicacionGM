using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using AppGM.Core.Delegados;

namespace AppGM.Core
{
    /// <summary>
    /// View model para una ventana en la que se hayan que seguir varios pasos
    /// </summary>
    public class ViewModelVentanaConPasos<TipoViewModel> : ViewModelMensajeBase
        where TipoViewModel: ViewModelVentanaConPasos<TipoViewModel>
    {
        #region Campos & Propiedades

        //-----------------------------------------CAMPOS----------------------------------------------------

        /// <summary>
        /// Funcion que dispara el evento property changed en <see cref="PuedeAvanzar"/> cada vez que una propiedad del VM cambia
        /// </summary>
        protected PropertyChangedEventHandler mHandlerPasoActualPropertyChanged;

        /// <summary>
        /// IndiceZ del paso en el que usuario se encuentra
        /// </summary>
        protected int mIndicePasoActual = 0;


        //---------------------------------------PROPIEDADES-------------------------------------------------

        /// <summary>
        /// Paso en el que el usuario se encuentra
        /// </summary>
        public ViewModelPaso<TipoViewModel> PasoActual => mViewModelsPasos[mIndicePasoActual];

        /// <summary>
        /// Indica si podemos pasar al proximo paso
        /// </summary>
        public bool PuedeAvanzar => mIndicePasoActual < mViewModelsPasos.Count - 1 && PasoActual.PuedeAvanzar();

        /// <summary>
        /// Indica si podemos retroceder
        /// </summary>
        public bool PuedeRetroceder => mIndicePasoActual > 0;

        /// <summary>
        /// Comando que se ejecuta al presionar el boton para avanzar de paso
        /// </summary>
        public ICommand ComandoPasoSiguiente { get; private set; }

        /// <summary>
        /// Comando que se ejecuta al presionar el boton para retroceder de paso
        /// </summary>
        public ICommand ComandoPasoAnterior { get; private set; }

        /// <summary>
        /// Lista de los pasos
        /// </summary>
        protected List<ViewModelPaso<TipoViewModel>> mViewModelsPasos = new List<ViewModelPaso<TipoViewModel>>();

        /// <summary>
        /// Evento que se llama cuando el usuario avanza de paso
        /// </summary>
        public event DVariableCambio<ViewModel> OnAvanzarPaso    = delegate { };

        /// <summary>
        /// Evento que se llama cuando el usuario retrocede de paso
        /// </summary>
        public event DVariableCambio<ViewModel> OnRetrocederPaso = delegate { };

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor default
        /// </summary>
        public ViewModelVentanaConPasos() {}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_viewModelsPasos">Lista de todos los pasos</param>
        public ViewModelVentanaConPasos(List<ViewModelPaso<TipoViewModel>> _viewModelsPasos)
        {
            mViewModelsPasos = _viewModelsPasos ?? new List<ViewModelPaso<TipoViewModel>>();

            Inicializar();
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

        protected void Inicializar()
        {
	        //Cuando se modifica una propiedad del viewmodel nos interesa volver a verificar si podemos avanzar
	        mHandlerPasoActualPropertyChanged = (sender, args) =>
	        {
		        //Si la propiedad que cambio es puede avanzar no disparamos el evento porque desencadenaria un bucle infinito
		        if (args.PropertyName != nameof(PuedeAvanzar))
			        DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuedeAvanzar)));
	        };

	        PasoActual.PropertyChanged += mHandlerPasoActualPropertyChanged;

            ComandoPasoSiguiente = new Comando(() => EstablecerIndiceActual(mIndicePasoActual + 1));
            ComandoPasoAnterior = new Comando(() => EstablecerIndiceActual(mIndicePasoActual - 1));
        } 
        #endregion
    }
}