using System.Windows.Controls;
using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// Lógica de interacción para UserControlPaginaPrincipalRol.xaml
    /// </summary>
    public partial class UserControlPaginaPrincipalRol : UserControl
    {
        public UserControlPaginaPrincipalRol()
        {
            InitializeComponent();

            DataContext = SistemaPrincipal.ObtenerInstancia<ViewModelPaginaPrincipalRol>();
        }
    }
}
