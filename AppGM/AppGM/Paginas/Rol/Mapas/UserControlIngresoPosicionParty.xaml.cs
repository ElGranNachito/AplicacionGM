using System.Windows.Controls;
using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// Lógica de interacción para UserControlIngresoPosicion.xaml UserControlIngresoPosicionParty.xaml
    /// </summary>
    public partial class UserControlIngresoPosicionParty : UserControl
    {
        public UserControlIngresoPosicionParty()
        {
            InitializeComponent();
        }

        private void TextBoxPosX_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(DataContext is ViewModelIngresoPosicion vm)
                vm.TextoPosicionX = ((TextBox)sender).Text;
        }

        private void TextBoxPosY_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is ViewModelIngresoPosicion vm)
                vm.TextoPosicionY = ((TextBox)sender).Text;
        }
    }
}
