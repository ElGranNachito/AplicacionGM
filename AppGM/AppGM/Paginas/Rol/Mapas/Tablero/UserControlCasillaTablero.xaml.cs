using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// Logica de interaccion para UserControlCasillaTablero.xaml
    /// </summary>
    public partial class UserControlCasillaTablero : UserControl
    {
        public UserControlCasillaTablero()
        {
            InitializeComponent();


            GridCasilla.MouseRightButtonDown += OnMouseRightButtonDown;

            GridCasilla.MouseEnter += OnMouseEnter;
            GridCasilla.MouseLeave += OnMouseLeave;
        }

        private void OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is ViewModelCasillaTablero vm)
                if (e.OriginalSource is Grid)
                {
                    vm.EstaSeleccionada = true;
                }
        }

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (DataContext is ViewModelCasillaTablero vm)
                if (e.OriginalSource is Grid)
                {
                    vm.ColorBordeCasilla = "00ffff";
                    vm.DispararPropertyChanged(new PropertyChangedEventArgs(nameof(vm.ColorBordeCasilla)));
                }
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (DataContext is ViewModelCasillaTablero vm)
                if (e.OriginalSource is Grid)
                {
                    vm.ColorBordeCasilla = "000000";
                    vm.DispararPropertyChanged(new PropertyChangedEventArgs(nameof(vm.ColorBordeCasilla)));
                }
        }
    }
}
