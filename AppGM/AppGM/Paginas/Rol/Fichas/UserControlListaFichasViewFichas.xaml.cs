using System.Windows.Controls;
using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// Lógica de interacción para UserControlListaFichasViewFichas.xaml
    /// </summary>
    public partial class UserControlListaFichasViewFichas : UserControl
    {
        public UserControlListaFichasViewFichas()
        {
            InitializeComponent();

            DataContext = SistemaPrincipal.ObtenerInstancia<ViewModelListaFichasVistaFichas>();
        }
    }
}
