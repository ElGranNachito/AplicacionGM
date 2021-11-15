using System.Windows.Controls;
using System.Windows.Input;

namespace AppGM
{
    /// <summary>
    /// Lógica de interacción para UserControlFichaItem.xaml
    /// </summary>
    public partial class UserControlFichaItem : UserControl
    {
        #region Campos

        /// <summary>
        /// Cantidad de veces el item es presionado con click izquierdo.
        /// </summary>
        private int cantidadClicks = 0;

        #endregion

        public UserControlFichaItem()
        {
            InitializeComponent();

            Gridsito.MouseEnter += OnMouseEnter;
            Gridsito.MouseLeave += OnMouseLeave;

            Gridsito.MouseLeftButtonDown += GridsitoOnMouseLeftButtonDown;
        }

        private void GridsitoOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is ViewModelFichaPersonaje vm)
            {
                ++cantidadClicks;

                if (cantidadClicks > 1)
                {
                    vm.VerFicha();

                    cantidadClicks = 0;
                }
            }
        }

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (DataContext is ViewModelFichaPersonaje vm)
                vm.EstaSeleccionada = true;
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (DataContext is ViewModelFichaPersonaje vm)
                vm.EstaSeleccionada = false;
        }
    }
}
