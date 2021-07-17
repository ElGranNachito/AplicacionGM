using System.Windows;
using System.Windows.Controls;

namespace AppGM
{
	/// <summary>
	/// Lógica de interacción para UserControlBloqueCondicional.xaml
	/// </summary>
	public partial class UserControlBloqueCondicional : UserControl
	{
		public UserControlBloqueCondicional()
		{
			InitializeComponent();
		}

		private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
		{
			((FrameworkElement) sender).DataContext = DataContext;
		}
	}
}
