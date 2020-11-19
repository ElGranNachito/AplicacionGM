using System.Windows;
using AppGM.Core;

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

            DataContextChanged += (obj, args) =>
            {
                if (ViewModel == null)
                    ViewModel = (ViewModelMapa) DataContext;
            };
        }

        private void OnMapaSizeXChanged(object sender, SizeChangedEventArgs e)
        {
            ViewModel.TamañoCanvasX = (float)e.NewSize.Width;
            ViewModel.TamañoCanvasY = (float)e.NewSize.Height;
        }

    }
}
