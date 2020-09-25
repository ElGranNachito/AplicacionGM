using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// Lógica de interacción para UserControlPaginaInicio.xaml
    /// </summary>
    public partial class UserControlPaginaInicio : BaseUserControl<ViewModelPaginaPrincipal>
    {
        public UserControlPaginaInicio()
        {
            InitializeComponent();

            ViewModel = SistemaPrincipal.ObtenerInstancia<ViewModelPaginaPrincipal>();
        }
    }
}
