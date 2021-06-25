using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using AppGM.Core;
using AppGM.Viewmodels;

namespace AppGM
{
    /// <summary>
    /// Clase abstracta que provee campos, propiedades y una implementacion por defecto de la interfaz <see cref="IVentana"/>
    /// para que hereden viewmodels destinados a representar una ventana
    /// </summary>
    public abstract class ViewModelVentanaBase : BaseViewModel, IVentana
    {
        #region Campos & Propiedades

        //---------------------------CAMPOS------------------------------


        /// <summary>
        /// Instancia de la ventana
        /// </summary>
        protected Window mVentana;

        /// <summary>
        /// Altura del titulo
        /// </summary>
        protected int    mCaptionHeight = 25;

        /// <summary>
        /// Titulo de la ventana
        /// </summary>
        protected string mTituloVentana = "Aplicacion GM";

        /// <summary>
        /// Altura del titulo
        /// </summary>
        protected GridLength mAlturaTitulo          = new GridLength(25);

        /// <summary>
        /// Grueso de los bordes para cambiar el tamaño de la ventana
        /// </summary>
        protected Thickness  mResizeBorderThickness = new Thickness(4);


        //----------------------------PROPIEDADES--------------------------------------


        /// <summary>
        /// Comando que se ejecutara al presionar la x de la ventana
        /// </summary>
        public ICommand ComandoCerrarVentana { get; set; }

        /// <summary>
        /// Propiedad que permite obtener la altura del titulo de la ventana. Esta propiedad esta destinada para ser usada en xaml solamente.
        /// Para modificar su valor hacerlo a traves de <see cref="CaptionHeight"/>
        /// </summary>
        public GridLength AlturaTitulo => new GridLength(mCaptionHeight);

        /// <summary>
        /// Propiedad que permite obtener y establecer el grosor de los bordes para cambiar de tamaño de la ventana
        /// </summary>
        public Thickness ResizeBorderThickness
        {
	        get => EstaMaximizada() ? new Thickness(0) : mResizeBorderThickness;
	        set => mResizeBorderThickness = value;
        }

        /// <summary>
        /// Propiedad que permite obtener y modificar el tamaño del titulo de la ventana
        /// </summary>
        public int CaptionHeight
        {
	        get => EstaMaximizada() ? 0 : mCaptionHeight;
	        set => mCaptionHeight = value;
        }

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor default
        /// </summary>
        /// <param name="_ventana">Instanacia de la ventana que representara este viewmodel</param>
        public ViewModelVentanaBase(Window _ventana)
        {
            mVentana = _ventana;

            //Cuando esta ventana es seleccionada por el usuario queremos hacerla la ventana actual
            mVentana.Activated += (sender, args) =>
            { 
	            SistemaPrincipal.Aplicacion.VentanaActual = (IVentana)((Window)sender).DataContext;
            };

            mVentana.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            mVentana.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            mVentana.Width = SystemParameters.PrimaryScreenWidth;
            mVentana.Height = SystemParameters.PrimaryScreenWidth;

            //Conectamos varios eventos de la ventana para que llamen a los delegados correspondientes de la interfaz IVentana
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

            //Inicializamos la propiedad en el constructor para poder acceder a 'this'
            VentanaMensaje = new Lazy<IVentanaMensaje>(
	            () =>
	            {
		            var ventanaMensaje          = new VentanaMensaje();
		            var viewModelVentanaMensaje = new ViewModelVentanaMensaje(ventanaMensaje);

		            viewModelVentanaMensaje.VentanaPadre = this;

		            ventanaMensaje.DataContext = viewModelVentanaMensaje;

		            return viewModelVentanaMensaje;

	            }, LazyThreadSafetyMode.PublicationOnly);
        }

        /// <summary>
        /// Constructor vacio por si alguna clase hijo necesita hacer algo distinto en los primeros pasos
        /// </summary>
        protected ViewModelVentanaBase() { }

		#endregion

		#region Metodos

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

		#endregion

		#region Implementacion Interfaz Ventana

		public virtual string TituloVentana
        {
	        get => mTituloVentana;
	        set
	        {
		        mTituloVentana = value;

		        OnTituloModificado(this);
	        }
        }

        public bool DebeEsperarCierreDeMensaje { get; set; }

        public bool EsVentanaActual => SistemaPrincipal.Aplicacion.VentanaActual == this;

		public Lazy<IVentanaMensaje> VentanaMensaje { get; set; }

        public virtual object ObtenerInstanciaVentana() => mVentana;

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

        public virtual Vector2 ObtenerTamaño() => new Vector2(mVentana.Width, mVentana.Height);
        public virtual bool EstaMaximizada() => mVentana.WindowState == WindowState.Maximized;

        //Eventos de la interfaz

        public event EventoVentana OnTamañoModificado     = delegate { };
        public event EventoVentana OnEstadoModificado     = delegate { };
        public event EventoVentana OnTituloModificado     = delegate { };
        public event EventoVentana OnMouseMovido          = delegate { };
        public event EventoVentana OnVentanaAbierta       = delegate { };
        public event EventoVentana OnVentanaCerrada       = delegate { };
        public event EventoVentana OnMouseDown            = delegate { };
        public event EventoVentana OnMouseUp              = delegate { };
        public event EventoVentana OnFotogramaActualizado = delegate { };

        #endregion
    }
}