using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using AppGM.Core;

namespace AppGM
{
    public abstract class ViewModelVentanaBase : BaseViewModel, IVentana
    {
        #region Miembros

        protected Window mVentana;

        protected int    mCaptionHeight = 25;
        protected string mTituloVentana = "Aplicacion GM";

        protected GridLength mAlturaTitulo          = new GridLength(25);
        protected Thickness  mResizeBorderThickness = new Thickness(4);
        public ICommand ComandoCerrarVentana { get; set; }

        #endregion

        #region Constructores
        /// <summary>
        /// Constructor default
        /// </summary>
        /// <param name="_ventana">Ventana</param>
        public ViewModelVentanaBase(Window _ventana)
        {
            mVentana = _ventana;

            mVentana.Closed    += (obj, e)        => { OnVentanaCerrada(this); };
            mVentana.Loaded    += (obj, e)    => { OnVentanaAbierta(this); };
            mVentana.MouseMove += (obj, e)     => { OnMouseMovido(this); };
            mVentana.MouseDown += (obj, e) => { OnMouseDown(this); };
            mVentana.MouseUp   += (obj, e) => { OnMouseUp(this); };

            ComandoCerrarVentana = new Comando(CerrarVentana);

            //Disparamos evento de cambio de propiedad para esas propiedades para que se modifique su visibilidad de acuerdo a
            //si la ventana esta maximizada o no
            mVentana.StateChanged += (obj, e) =>
            {
                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(AlturaTitulo)));
                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(ResizeBorderThickness)));
                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(CaptionHeight)));
            };
        }

        /// <summary>
        /// Constructor vacion por si alguna clase hijo necesita hacer algo distinto en los primeros pasos
        /// </summary>
        protected ViewModelVentanaBase() { } 

        #endregion

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

        #endregion

        #region Implementacion Interfaz Ventana

        public virtual void CerrarVentana() => mVentana.Close();

        public virtual void Maximizar()
        {
            mVentana.WindowState = WindowState.Maximized;

            OnEstadoModificado(this);
        }

        public virtual void Minimizar()
        {
            mVentana.WindowState = WindowState.Minimized;

            OnEstadoModificado(this);
        }

        public virtual void Normalizar()
        {
            mVentana.WindowState = WindowState.Normal;

            OnEstadoModificado(this);
        }

        public virtual Vector2 ObtenerTamaño() => new Vector2(mVentana.Width, mVentana.Height);

        public virtual void EstablecerTamañoX(float x)
        {
            mVentana.Width = x;

            OnTamañoModificado(this);
        }

        public virtual void EstablecerTamañoY(float y)
        {
            mVentana.Height = y;

            OnTamañoModificado(this);
        }

        public virtual void EstablecerTamaño(Vector2 nuevoTamaño)
        {
            mVentana.Width = nuevoTamaño.X;
            mVentana.Height = nuevoTamaño.Y;

            OnTamañoModificado(this);
        }
        public virtual Vector2 ObtenerPosicionDelMouse()
        {
            Point v = Mouse.GetPosition(mVentana);

            return new Vector2(v.X, v.Y);
        }
        public virtual bool EstaMaximizada() => mVentana.WindowState == WindowState.Maximized;
        public virtual string TituloVentana
        {
            get => mTituloVentana;
            set
            {
                mTituloVentana = value;

                OnTituloModificado(this);
            }
        }
        protected void DispararEvento(string nombre)
        {
            switch (nombre)
            {
                case nameof(OnTamañoModificado):
                    OnTamañoModificado(this);
                    break;
                case nameof(OnEstadoModificado):
                    OnEstadoModificado(this);
                    break;
                case nameof(OnTituloModificado):
                    OnTituloModificado(this);
                    break;
                case nameof(OnMouseMovido):
                    OnMouseMovido(this);
                    break;
                case nameof(OnVentanaAbierta):
                    OnVentanaAbierta(this);
                    break;
                case nameof(OnVentanaCerrada):
                    OnVentanaCerrada(this);
                    break;
            }
        }

        public event EventoVentana OnTamañoModificado = delegate { };
        public event EventoVentana OnEstadoModificado = delegate { };
        public event EventoVentana OnTituloModificado = delegate { };
        public event EventoVentana OnMouseMovido      = delegate { };
        public event EventoVentana OnVentanaAbierta   = delegate { };
        public event EventoVentana OnVentanaCerrada   = delegate { };
        public event EventoVentana OnMouseDown        = delegate { };
        public event EventoVentana OnMouseUp          = delegate { };
        public event EventoVentana OnFotogramaActualizado = delegate { };

        #endregion
    }
}