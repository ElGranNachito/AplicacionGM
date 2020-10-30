using System.ComponentModel;
using System.Reflection;
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
    class ViewModelVentanaPrincipal : BaseViewModel, IVentana
    {
        #region Miembros Privados

        private Window        mVentana;

        private int           mCaptionHeight         = 5;
        private string        mTituloVentana         = "Aplicacion GM";

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

            ComandoCerrarVentana    = new Comando(CerrarVentana);
            ComandoMinimizarVentana = new Comando(()=> mVentana.WindowState = WindowState.Minimized);
            ComandoMaximizarVentana = new Comando(()=> mVentana.WindowState = EstaMaximizada()
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

            mVentana.MouseMove += (obj, e) => { OnMouseMovido(this); };

            SistemaPrincipal.Aplicacion.PropertyChanged += (o, e) =>
            {
                DispararPropertyChanged(e);
            };
        }

        #region Propiedades
        public GridLength AlturaTitulo
        {
            get => EstaMaximizada() ? new GridLength(0) : mAlturaTitulo;
            set => mAlturaTitulo = value;
        }
        public Thickness ResizeBorderThickness
        {
            get => EstaMaximizada() ? new Thickness(0) : mResizeBorderThickness;
            set => mResizeBorderThickness = value;
        }

        public int CaptionHeight
        {
            get => EstaMaximizada() ? 0 : mCaptionHeight;
            set => mCaptionHeight = value;
        }
        public EPaginaActual EPaginaActual
        {
            get => SistemaPrincipal.Aplicacion.EPaginaActual;
        }

        #endregion

        #region Implementacion Interfaz Ventana

        public event EventoVentana OnTamañoModificado = delegate{};
        public event EventoVentana OnEstadoModificado = delegate{};
        public event EventoVentana OnTituloModificado = delegate{};
        public event EventoVentana OnMouseMovido      = delegate{};
        public string TituloVentana
        {
            get => mTituloVentana;
            set
            {
                mTituloVentana = value;

                OnTituloModificado(this);
            }
        }

        public void CerrarVentana() => mVentana.Close();

        public void Maximizar()
        {
            mVentana.WindowState = WindowState.Maximized;

            OnEstadoModificado(this);
        }

        public void Minimizar()
        {
            mVentana.WindowState = WindowState.Minimized;

            OnEstadoModificado(this);
        }

        public void Normalizar()
        {
            mVentana.WindowState = WindowState.Normal;

            OnEstadoModificado(this);
        }

        public Vector2 ObtenerTamaño() => new Vector2(mVentana.Width, mVentana.Height);

        public void EstablecerTamañoX(float x)
        {
            mVentana.Width = x;

            OnTamañoModificado(this);
        }

        public void EstablecerTamañoY(float y)
        {
            mVentana.Height = y;

            OnTamañoModificado(this);
        }

        public void EstablecerTamaño(Vector2 nuevoTamaño)
        {
            mVentana.Width = nuevoTamaño.X;
            mVentana.Height = nuevoTamaño.Y;

            OnTamañoModificado(this);
        }
        public Vector2 ObtenerPosicionDelMouse()
        {
            Point v = Mouse.GetPosition(mVentana);

            return new Vector2(v.X, v.Y);
        }
        public bool EstaMaximizada() => mVentana.WindowState == WindowState.Maximized;

        #endregion
    }
}
