using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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

            //Si se trata del administrador de combates la propiedad viewmodel no se inicializa al momento de la creacion
            //asi que para evitar nullreference esperamos a que se establezca el datacontext y luego establecemos el viewmodel
            DataContextChanged += (obj, args) =>
            {
                if (ViewModel == null)
                    ViewModel = (ViewModelMapa) DataContext;
            };

            Gridsito.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
        }

        private void OnMapaSizeXChanged(object sender, SizeChangedEventArgs e)
        {
            ViewModel.TamañoCanvasX = (float)e.NewSize.Width;
            ViewModel.TamañoCanvasY = (float)e.NewSize.Height;
        }

        private void OnPreviewMouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            if (!Keyboard.IsKeyDown(Key.LeftCtrl) && 
                e.OriginalSource is not UserControlIngresoPosicion && 
                e.OriginalSource is not UserControlIngresoPosicionParty && 
                e.OriginalSource is not UserControlIngresoPosicionGeneral &&
                e.OriginalSource is not TextBox)
                ViewModel.DeseleccionarUnidades();
        }
    }
}
