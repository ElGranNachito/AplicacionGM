using System.Windows.Controls;
using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// Lógica de interacción para MenuSeleccionCombate.xaml
    /// </summary>
    public partial class UserControlMenuSeleccionCombate : UserControl
    {
        public UserControlMenuSeleccionCombate()
        {
            DataContext = SistemaPrincipal.MenuSeleccionCombate;

            InitializeComponent();
        }
    }
}
