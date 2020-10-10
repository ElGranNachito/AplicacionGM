using System.Windows;
using System.Windows.Controls;
using AppGM.Core;
using AppGM.Viewmodels;

namespace AppGM
{
    /// <summary>
    /// Lógica de interacción para UserControlMapa.xaml
    /// </summary>
    public partial class UserControlMapa : BaseUserControl<ViewModelMapa>
    {
        public UserControlMapa()
        {
            InitializeComponent();
        }

        private void OnMapaSizeXChanged(object sender, SizeChangedEventArgs e)
        {
            ViewModel.TamañoCanvasX = (float)e.NewSize.Width;
            ViewModel.TamañoCanvasY = (float)e.NewSize.Height;
        }
    }
}
