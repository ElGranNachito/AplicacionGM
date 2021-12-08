using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;

namespace AppGM
{
	/// <summary>
	/// Lógica de interacción para UserControlListaOrdenableItem.xaml
	/// </summary>
	public partial class UserControlListaOrdenableItem : UserControl
	{
		public UserControlListaOrdenableItem()
		{
			InitializeComponent();
		}

		public static readonly DependencyProperty DockControlOrdenProperty = 
			DependencyProperty.Register(
				"DockControlOrden", 
				typeof(DockPosition), 
				typeof(UserControlListaOrdenableItem),
				new PropertyMetadata(DockPosition.Right));

		/// <summary>
		/// Posicion del control de modificacion del orden
		/// </summary>
		public DockPosition DockControlOrden
		{
			get => (DockPosition)GetValue(DockControlOrdenProperty);
			set => SetValue(DockControlOrdenProperty, value);
		}
	}
}
