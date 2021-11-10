using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace AppGM
{
    /// <summary>
    /// Lógica de interacción para UserControlComboBoxConDescripcion.xaml
    /// </summary>
    public partial class UserControlComboBoxConDescripcion : UserControl
    {
        public UserControlComboBoxConDescripcion() => InitializeComponent();

        /// <summary>
        /// Contiene la descipcion
        /// </summary>
        public static readonly DependencyProperty DescripcionProperty =
            DependencyProperty.Register("Descripcion",
                typeof(string),
                typeof(UserControlComboBoxConDescripcion),
                new PropertyMetadata(OnDescripcionChanged));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource",
                typeof(IEnumerable),
                typeof(UserControlComboBoxConDescripcion),
                new PropertyMetadata(OnItemsSourceChanged));

        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue",
                typeof(object),
                typeof(UserControlComboBoxConDescripcion),
                new PropertyMetadata(OnSelectedValueChanged));

        public string Descripcion
        {
            get => (string)GetValue(DescripcionProperty);
            set => SetValue(DescripcionProperty, value);
        }

        public IEnumerable ItemsSource
        {
            get => (IEnumerable) GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public object SelectedValue
        {
            get => GetValue(SelectedValueProperty);
            set => SetValue(SelectedValueProperty, value);
        }

        private static void OnDescripcionChanged(DependencyObject dp, DependencyPropertyChangedEventArgs args)
        {
           
        }

        private static void OnItemsSourceChanged(DependencyObject dp, DependencyPropertyChangedEventArgs args)
        {
            if (dp is UserControlComboBoxConDescripcion userControl)
                userControl.ComboBox.ItemsSource = (IEnumerable) args.NewValue;
        }

        private static void OnSelectedValueChanged(DependencyObject dp, DependencyPropertyChangedEventArgs args)
        {
            //if (dp is UserControlComboBoxConDescripcion userControl)
            //    userControl.ComboBox.DataContext = args.NewValue;
        }
    }
}
