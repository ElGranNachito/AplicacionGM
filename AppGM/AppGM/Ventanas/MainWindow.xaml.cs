using System.Windows;
using AppGM.Core;
using AppGM.Viewmodels;
using CoolLogs;

namespace AppGM
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            SistemaPrincipal.LoggerGlobal.Log("Construyendo ventana principal...", ESeveridad.Info);

	        //Creamos los view models para la ventana principal y los popups
            IVentana        ventanaPrincipal     = new ViewModelVentanaPrincipal(this);

            //Establecemos la ventana principal como esta y la de los popups
            SistemaPrincipal.Aplicacion.VentanaPrincipal = ventanaPrincipal;

            DataContext    = ventanaPrincipal;

            SistemaPrincipal.Aplicacion.PaginaActual = EPagina.PaginaPrincipal;

            InitializeComponent();

            SistemaPrincipal.LoggerGlobal.Log("Ventana principal lista", ESeveridad.Info);
        }
    }
}