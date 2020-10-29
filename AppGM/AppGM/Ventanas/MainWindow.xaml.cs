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

            DataContext = new ViewModelVentanaPrincipal(this);
            SistemaPrincipal.Aplicacion.VentanaPrincipal = (IVentana)DataContext;
        }
    }
}