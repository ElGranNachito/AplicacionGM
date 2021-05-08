using System.Windows;
using System.Windows.Input;
using AppGM.Core;

namespace AppGM.Viewmodels
{
    /// <summary>
    /// View model para la ventana principal de la aplicacion
    /// </summary>
    class ViewModelVentanaPrincipal : ViewModelVentanaBase, IVentana
    {
        #region Miembros Publicos
        public ICommand ComandoMaximizarVentana { get; set; }
        public ICommand ComandoMinimizarVentana { get; set; }
        public IVentanaMensaje VentanaPopups => SistemaPrincipal.Aplicacion.VentanaPopups;

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
        
        #region Propiedades
        public EPagina EPagina => SistemaPrincipal.Aplicacion.EPagina;

        #endregion
    }
}
