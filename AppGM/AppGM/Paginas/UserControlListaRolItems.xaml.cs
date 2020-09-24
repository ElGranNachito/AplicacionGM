
using System.Windows.Controls;
using AppGM.Core;
using AppGMCore;


namespace AppGM
{
    /// <summary>
    /// Lógica de interacción para UserControlListaRolItems.xaml
    /// </summary>
    public partial class UserControlListaRolItems : UserControl
    {
        public UserControlListaRolItems()
        {
            InitializeComponent();

            DataContext = new ViewModelListaRoles();
        }
    }
}
