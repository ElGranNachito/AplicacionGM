using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AppGM
{
	/// <summary>
	/// Lógica de interacción para UserControlConImagencitaAlCostado.xaml
	/// </summary>
	public partial class UserControlConImagencitaAlCostado : UserControl
	{
		public UserControlConImagencitaAlCostado()
		{
			InitializeComponent();
		}

		public static readonly DependencyProperty SourceProperty =
			DependencyProperty.Register("Source", typeof(ImageSource), typeof(UserControlConImagencitaAlCostado));

		public ImageSource Source
		{
			get => GetValue(SourceProperty) as ImageSource;
			set => SetValue(SourceProperty, value);
		}
	}
}
