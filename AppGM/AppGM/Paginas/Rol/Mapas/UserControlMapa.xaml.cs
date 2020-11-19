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
        }

        private void OnMapaSizeXChanged(object sender, SizeChangedEventArgs e)
        {
            ViewModel = (ViewModelMapa) DataContext;

            ViewModel.TamañoCanvasX = (float)e.NewSize.Width;
            ViewModel.TamañoCanvasY = (float)e.NewSize.Height;
        }

        public static readonly DependencyProperty VMProperty = DependencyProperty.Register("VM", typeof(ViewModelMapa), typeof(UserControlMapa), new PropertyMetadata(VMChanged));

        public ViewModelMapa VM
        {
            get => (ViewModelMapa)GetValue(VMProperty);
            set => SetValue(VMProperty, value);
        }
        private static void VMChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ((UserControlMapa)(FrameworkElement) obj).ViewModel = (ViewModelMapa)args.NewValue;
        }

    }
}
