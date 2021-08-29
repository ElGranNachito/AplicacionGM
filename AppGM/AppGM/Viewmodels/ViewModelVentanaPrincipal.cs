using System;
using System.Windows;
using System.Windows.Input;
using AppGM.Core;

namespace AppGM.Viewmodels
{
    /// <summary>
    /// View model para la ventana principal de la aplicacion
    /// </summary>
    class ViewModelVentanaPrincipal : ViewModelVentanaBase
    {
        #region Propiedades

        public ICommand ComandoMaximizarVentana { get; set; }
        public ICommand ComandoMinimizarVentana { get; set; }

        /// <summary>
        /// Pagina actual de la ventana principal
        /// </summary>
        public EPagina PaginaActual => SistemaPrincipal.Aplicacion.PaginaActual;

        public override Lazy<IVentanaMensaje> VentanaMensaje { get; set; } = new Lazy<IVentanaMensaje>(() => SistemaPrincipal.Aplicacion.VentanaMensajePrincipal);

        public double Altura => System.Windows.SystemParameters.PrimaryScreenHeight;
        public double Ancho => System.Windows.SystemParameters.PrimaryScreenWidth;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ventana">Ventana principal de la aplicacion</param>
        public ViewModelVentanaPrincipal(Window _ventana) : base(_ventana)
        {
	        ComandoCerrarVentana    = new Comando(() => Application.Current.Shutdown(0));
            ComandoMinimizarVentana = new Comando(() => mVentana.WindowState = WindowState.Minimized);
            ComandoMaximizarVentana = new Comando(() => mVentana.WindowState = EstaMaximizada()
                ? WindowState.Normal
                : WindowState.Maximized);
            
            SistemaPrincipal.Aplicacion.PropertyChanged += (o, e) =>
            {
                DispararPropertyChanged(e);
            };
        }
    }
}
