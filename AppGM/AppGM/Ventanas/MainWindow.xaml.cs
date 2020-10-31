using System.Windows;
using AppGM.Core;
using AppGM.Viewmodels;

namespace AppGM
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Creamos la ventana para los popups
            VentanaMensaje vm = new VentanaMensaje();

            //Creamos los view models para la ventana principal y los popups
            IVentana        ventanaPrincipal     = new ViewModelVentanaPrincipal(this);
            IVentanaMensaje ventanaPopups        = new ViewModelVentanaMensaje(vm);

            //Establecemos la ventana principal como esta y la de los popups
            SistemaPrincipal.Aplicacion.VentanaPrincipal = ventanaPrincipal;
            SistemaPrincipal.Aplicacion.VentanaPopups    = ventanaPopups;

            //Establecemos los viewm models de las ventanas
            vm.DataContext = ventanaPopups;
            DataContext    = ventanaPrincipal;
        }
    }
}