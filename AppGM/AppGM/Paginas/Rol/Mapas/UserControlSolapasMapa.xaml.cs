using System.Windows.Controls;
using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// Lógica de interacción para UserControlSolapasMapa.xaml
    /// </summary>
    public partial class UserControlSolapasMapa : UserControl
    {
        public UserControlSolapasMapa()
        {
            InitializeComponent();

            DataContext = SistemaPrincipal.RolSeleccionado.SeccionMapaSeleccionada;
        }
    }
}
