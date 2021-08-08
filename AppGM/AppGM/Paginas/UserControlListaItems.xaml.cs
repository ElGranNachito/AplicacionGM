using System.Windows;
using System.Windows.Controls;

namespace AppGM
{
    /// <summary>
    /// Lógica de interacción para UserControlListaItems.xaml
    /// </summary>
    public partial class UserControlListaItems : UserControl
    {
        public UserControlListaItems()
        {
            InitializeComponent();
        }

        private static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(
	        "ItemTemplate",
	        typeof(DataTemplate),
	        typeof(UserControlListaItems), new PropertyMetadata(defaultValue: null));

        public DataTemplate ItemTemplate
        {
	        get => GetValue(ItemTemplateProperty) as DataTemplate;
	        set => SetValue(ItemTemplateProperty, value);
        }
    }
}
