using System.Windows.Controls;
using AppGM.Core;


namespace AppGM
{
    /// <summary>
    /// Interaction logic for UserControlIngresoPosicionGeneral.xaml
    /// </summary>
    public partial class UserControlIngresoPosicionGeneral : UserControl
    {
        public UserControlIngresoPosicionGeneral()
        {
            InitializeComponent();
        }

        private void TextBoxPosX_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is ViewModelMapa vm)
            {
                for (int i = 0; i < vm.UnidadesSeleccionadas.Count; ++i)
                    vm.UnidadesSeleccionadas[i].TextoPosicionX = ((TextBox)sender).Text;
            }
        }

        private void TextBoxPosY_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is ViewModelMapa vm)
            {
                for (int i = 0; i < vm.UnidadesSeleccionadas.Count; ++i)
                    vm.UnidadesSeleccionadas[i].TextoPosicionY = ((TextBox)sender).Text;
            }
        }
    }
}
