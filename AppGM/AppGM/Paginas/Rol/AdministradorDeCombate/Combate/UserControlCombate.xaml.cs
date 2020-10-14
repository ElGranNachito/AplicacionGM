using System.Windows.Controls;
using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// Lógica de interacción para UserControlCombate.xaml
    /// </summary>
    public partial class UserControlCombate : UserControl
    {
        public UserControlCombate()
        {
            InitializeComponent();

            DataContext = SistemaPrincipal.CombateActual;
        }
    }
}
