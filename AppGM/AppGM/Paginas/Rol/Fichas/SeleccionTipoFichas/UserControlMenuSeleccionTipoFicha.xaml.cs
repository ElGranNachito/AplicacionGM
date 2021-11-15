using System.Windows.Controls;
using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// Lógica de interacción para UserControlMenuSeleccionTipoFicha.xaml
    /// </summary>
    public partial class UserControlMenuSeleccionTipoFicha : UserControl
    {
        public UserControlMenuSeleccionTipoFicha()
        {
            InitializeComponent();

            DataContext = SistemaPrincipal.ObtenerInstancia<ViewModelMenuSeleccionFicha>();
        }
    }
}
