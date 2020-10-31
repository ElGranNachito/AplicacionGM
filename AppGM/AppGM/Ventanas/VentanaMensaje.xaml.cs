using System.Windows;
using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// Lógica de interacción para VentanaMensaje.xaml
    /// </summary>
    public partial class VentanaMensaje : Window
    {
        public VentanaMensaje()
        {
            DataContext = SistemaPrincipal.Aplicacion.VentanaPopups;

            InitializeComponent();
        }
    }
}
