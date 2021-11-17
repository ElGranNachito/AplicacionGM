using System.Windows;
using System.Windows.Controls;

namespace AppGM
{
	/// <summary>
	/// Lógica de interacción para UserControlItemLista.xaml
	/// </summary>
	public partial class UserControlItemLista : UserControl
	{
		/// <summary>
		/// Propiedad que indica si se debe mostrar la version reducida del item
		/// </summary>
		public static readonly DependencyProperty MostrarVersionReducidaProperty = 
			DependencyProperty.Register("MostarVersionReducida", typeof(bool), typeof(UserControlItemLista), new PropertyMetadata(false));

		public UserControlItemLista()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Obtiene o establece el valor de <see cref="MostrarVersionReducidaProperty"/> para este objeto
		/// </summary>
		public bool MostrarVersionReducida
		{
			get => (bool)GetValue(MostrarVersionReducidaProperty);
			set => SetValue(MostrarVersionReducidaProperty, value);
		}
	}
}
