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
	        typeof(UserControlListaItems), 
	        new PropertyMetadata(defaultValue: new DataTemplate
	        {
		        DataType = typeof(UserControlItemLista),
                VisualTree = new FrameworkElementFactory(typeof(UserControlItemLista))
	        }));

        /// <summary>
        /// <see cref="DataTemplate"/> de los items contenidos en esta lista
        /// </summary>
        public DataTemplate ItemTemplate
        {
	        get => GetValue(ItemTemplateProperty) as DataTemplate;
	        set => SetValue(ItemTemplateProperty, value);
        }

        private static readonly DependencyProperty TamañoFontTituloProperty = DependencyProperty.Register(
            "TamañoFontTitulo",
	        typeof(int),
	        typeof(UserControlListaItems), new PropertyMetadata(defaultValue: 10));

        /// <summary>
        /// Tamaño del font del titulo de esta lista
        /// </summary>
        public int TamañoFontTitulo
        {
	        get => (int)GetValue(TamañoFontTituloProperty);
	        set => SetValue(TamañoFontTituloProperty, value);
        }
    }
}
