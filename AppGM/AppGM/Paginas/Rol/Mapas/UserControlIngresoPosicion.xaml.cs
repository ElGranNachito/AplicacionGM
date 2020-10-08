using System.Windows;
using System.Windows.Controls;
using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// Lógica de interacción para UserControlIngresoPosicion.xaml
    /// </summary>
    public partial class UserControlIngresoPosicion : UserControl
    {
        public UserControlIngresoPosicion()
        {
            InitializeComponent();
        }

        private void TextBoxPosX_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((ViewModelIngresoPosicion)DataContext).TextoPosicionX = ((TextBox)sender).Text;
        }

        private void TextBoxPosY_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((ViewModelIngresoPosicion)DataContext).TextoPosicionY = ((TextBox)sender).Text;
        }
    }
}
