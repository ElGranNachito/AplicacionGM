using System.Windows.Controls;
using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// Logica de interaccion para UserControlClimaHorario.xaml
    /// </summary>
    public partial class UserControlClimaHorario : UserControl
    {
        public UserControlClimaHorario()
        {
            InitializeComponent();
        }

        private void TextBoxBaseHora_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is ViewModelClimaHorario vm)
            {
                vm.Hora = ((TextBox)sender).Text;
            }
        }

        private void TextBoxBaseMinuto_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is ViewModelClimaHorario vm)
            {
                vm.Minuto = ((TextBox)sender).Text;
            }
        }
    }
}
