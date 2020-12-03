using System.ComponentModel;
using System.Windows.Controls;
using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// Lógica de interacción para UserControlCreacionRol_DatosRol.xaml
    /// </summary>
    public partial class UserControlCreacionRol_DatosRol : BaseUserControl<ViewModelMensajeCrearRol_DatosRol>
    {
        public UserControlCreacionRol_DatosRol()
        {
            InitializeComponent();
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.DescripcionRol = ((TextBox) sender).Text;

            ViewModel.DispararPropertyChanged(new PropertyChangedEventArgs(nameof(ViewModelMensajeCrearRol_DatosRol.TextoLetrasRestantes)));
        } 
    }
}
