using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using AppGM.Core;
using AppGM.Helpers;

namespace AppGM.Viewmodels
{
    /// <summary>
    /// View model para la ventana principal de la aplicacion
    /// </summary>
    class ViewModelVentanaPrincipal : BaseViewModel
    {
        #region Miembros Privados

        private Window        mVentana;

        private int           mCaptionHeight         = 5;

        private GridLength    mAlturaTitulo          = new GridLength(25);
        private Thickness     mResizeBorderThickness = new Thickness(2);

        #endregion

        #region Miembros Publicos

        public ICommand ComandoCerrarVentana    { get; set; }
        public ICommand ComandoMaximizarVentana { get; set; }
        public ICommand ComandoMinimizarVentana { get; set; }

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ventana">Ventana principal de la aplicacion</param>
        public ViewModelVentanaPrincipal(Window ventana)
        {
            mVentana = ventana;

            ComandoCerrarVentana    = new Comando(()=> mVentana.Close());
            ComandoMinimizarVentana = new Comando(()=> mVentana.WindowState = WindowState.Minimized);
            ComandoMaximizarVentana = new Comando(()=> mVentana.WindowState = mVentana.EstaMaximizada()
                    ? WindowState.Normal
                    : WindowState.Maximized);

            //Disparamos evento de cambio de propiedad para esas propiedades para que se modifique su visibilidad de acuerdo a
            //si la ventana esta maximizada o no
            mVentana.StateChanged += (obj, e) =>
            {
                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(AlturaTitulo)));
                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(ResizeBorderThickness)));
                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(CaptionHeight)));
            };

            SistemaPrincipal.ObtenerInstancia<ViewModelAplicacion>().PropertyChanged += (obj, e) =>
            {
                DispararPropertyChanged(new PropertyChangedEventArgs(e.PropertyName));
            };
        }

        #region Propiedades
        public GridLength AlturaTitulo
        {
            get => mVentana.EstaMaximizada() ? new GridLength(0) : mAlturaTitulo;
            set => mAlturaTitulo = value;
        }
        public Thickness ResizeBorderThickness
        {
            get => mVentana.EstaMaximizada() ? new Thickness(0) : mResizeBorderThickness;
            set => mResizeBorderThickness = value;
        }

        public int CaptionHeight
        {
            get => mVentana.EstaMaximizada() ? 0 : mCaptionHeight;
            set => mCaptionHeight = value;
        }

        public string TituloVentana
        {
            get => SistemaPrincipal.ObtenerInstancia<ViewModelAplicacion>().TituloVentana;
        }

        public EPaginaActual EPaginaActual
        {
            get => SistemaPrincipal.ObtenerInstancia<ViewModelAplicacion>().EPaginaActual;
        }

        #endregion
    }
}
