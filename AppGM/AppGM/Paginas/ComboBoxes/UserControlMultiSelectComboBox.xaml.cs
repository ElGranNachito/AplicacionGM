using System.Windows;
using System.Windows.Controls;

namespace AppGM
{
	/// <summary>
	/// Lógica de interacción para UserControlMultiSelectComboBox.xaml
	/// </summary>
	public partial class UserControlMultiSelectComboBox : UserControl
	{
		public UserControlMultiSelectComboBox()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Contiene la descipcion
		/// </summary>
		public static readonly DependencyProperty DescripcionProperty =
			DependencyProperty.Register("Descripcion_MultiselectComboBox",
				typeof(string),
				typeof(UserControlComboBoxConDescripcion),
				new PropertyMetadata("Nada"));

		public string Descripcion
		{
			get => (string)GetValue(DescripcionProperty);
			set => SetValue(DescripcionProperty, value);
		}
	}
}
