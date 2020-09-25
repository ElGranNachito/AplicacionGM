using System.Windows.Controls;
using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// Lógica de interacción para UserControlSeleccionTipoFicha.xaml
    /// </summary>
    public partial class UserControlSeleccionTipoFicha : UserControl
    {
        public UserControlSeleccionTipoFicha()
        {
            InitializeComponent();

            DataContext = SistemaPrincipal.ObtenerInstancia<ViewModelMenuSeleccionTipoFicha>();
        }
    }
}
