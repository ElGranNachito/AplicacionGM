using System.ComponentModel;
using System.Windows.Controls;
using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// Logica de interaccion para UserControlCrearAccionParticipante.xaml
    /// </summary>
    public partial class UserControlCrearAccionParticipante : UserControl
    {
        public UserControlCrearAccionParticipante()
        {
            InitializeComponent();
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is ViewModelCrearAccionParticipante vm)
            {
                vm.DescripcionAccion = ((TextBox)sender).Text;

                vm.DispararPropertyChanged(new PropertyChangedEventArgs(nameof(ViewModelCrearAccionParticipante.TextoLetrasRestantesDescripcion)));
            }
        }
    }
}
